using AlexGuitarsShop.DAL;
using Microsoft.AspNetCore.Mvc;
using AlexGuitarsShop.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using AlexGuitarsShop.Domain.Interfaces.Account;

namespace AlexGuitarsShop.Controllers;

public class AccountController : Controller
{
    private const int Limit = 10;

    private readonly IAccountsCreator _accountsCreator;
    private readonly IAccountsProvider _accountsProvider;
    private readonly IAccountsUpdater _accountsUpdater;

    private int _pageCount;
    private int _offset;

    public AccountController(IAccountsCreator accountsCreator, 
        IAccountsProvider accountsProvider, IAccountsUpdater accountsUpdater)
    {
        _accountsCreator  = accountsCreator;
        _accountsProvider  = accountsProvider;
        _accountsUpdater  = accountsUpdater;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (_accountsCreator == null) return View(model);
        var response = await _accountsCreator.AddAccountAsync(model)!;
        if (response!.StatusCode == Domain.StatusCode.Ok)
        {
            await ValidUserAuthorizer.SignIn(HttpContext, response.Data);
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", response.Description!);

        return View(model);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (_accountsProvider == null) return View(model);
        var response = await _accountsProvider.GetAccountAsync(model)!;
        if (response is {StatusCode: Domain.StatusCode.Ok})
        {
            await ValidUserAuthorizer.SignIn(HttpContext, response.Data);
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", response!.Description!);

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await ValidUserAuthorizer.SignOut(HttpContext);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Users(Role role, int pageNumber = 1)
    {
        _offset = (pageNumber - 1) * Limit;
        int count = role == Role.User
            ? (await _accountsProvider!.GetAdminsCountAsync()!)!.Data
            : (await _accountsProvider!.GetUsersCountAsync()!)!.Data;
        _pageCount = count % Limit == 0 ? count / Limit : count / Limit + 1;
        var response = role == Role.Admin
            ? await _accountsProvider.GetAdminsAsync(_offset, Limit)!
            : await _accountsProvider.GetUsersAsync(_offset, Limit)!;
        if (response is {StatusCode: Domain.StatusCode.Ok})
        {
            UserListViewModel model = new UserListViewModel
            {
                Users = response.Data, Role = role, PageCount = _pageCount, CurrentPage = pageNumber
            };

            return View(model);
        }

        ViewBag.Message = response!.Description;
        return View("Notification");
    }

    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> MakeAdmin(string email)
    {
        if (_accountsUpdater != null) await _accountsUpdater.SetAdminRightsAsync(email)!;
        return RedirectToAction("Users", new {role = "Users"});
    }

    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> MakeUser(string email)
    {
        if (_accountsUpdater != null) await _accountsUpdater.RemoveAdminRightsAsync(email)!;
        return RedirectToAction("Users", new {role = "Admins"});
    }
}