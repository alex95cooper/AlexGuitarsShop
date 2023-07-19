using System.Security.Claims;
using AlexGuitarsShop.Domain.ViewModels;
using AlexGuitarsShop.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

public class AccountController : Controller
{
    const int Limit = 10;

    private readonly IAccountService _accountService;

    private int _pageCount;
    private int _offset;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var response = await _accountService.Register(model);
        if (response.StatusCode == Domain.Enums.StatusCode.OK)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response.Data));

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", response.Description);
        return View(model);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var response = await _accountService.Login(model);
        if (response.StatusCode == Domain.Enums.StatusCode.OK)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response.Data));

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", response.Description);
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Users(string role, int pageNumber = 1)
    {
        _offset = (pageNumber - 1) * Limit;
        int count = role == "Admins"
            ? (await _accountService.GetAdminsCount()).Data
            : (await _accountService.GetUsersCount()).Data;
        _pageCount = count % Limit == 0 ? count / Limit : count / Limit + 1;
        var response = role == "Admins"
            ? await _accountService.GetAdmins(_offset, Limit)
            : await _accountService.GetUsersOnly(_offset, Limit);
        if (response.StatusCode == Domain.Enums.StatusCode.OK)
        {
            UserListViewModel model = new UserListViewModel
            {
                Users = response.Data, Role = role, PageCount = _pageCount, CurrentPage = pageNumber
            };

            return View(model);
        }

        ViewBag.Message = response.Description;
        return View("Notification");
    }

    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> MakeAdmin(string email)
    {
        await _accountService.SetAdminRights(email);
        return RedirectToAction("Users", new {role = "Users"});
    }

    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> MakeUser(string email)
    {
        await _accountService.RemoveAdminRights(email);
        return RedirectToAction("Users", new {role = "Admins"});
    }
}