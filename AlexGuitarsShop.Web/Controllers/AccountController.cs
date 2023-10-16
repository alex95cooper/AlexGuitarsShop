using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AlexGuitarsShop.Web.Domain.Interfaces.Account;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Controllers;

public class AccountController : Controller
{
    private readonly IAccountsCreator _accountsCreator;
    private readonly IAccountsProvider _accountsProvider;
    private readonly IAccountsUpdater _accountsUpdater;
    private readonly IAuthorizer _authorizer;

    public AccountController(IAccountsCreator accountsCreator, IAccountsProvider accountsProvider,
        IAccountsUpdater accountsUpdater, IAuthorizer authorizer)
    {
        _accountsCreator = accountsCreator;
        _accountsProvider = accountsProvider;
        _accountsUpdater = accountsUpdater;
        _authorizer = authorizer;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return GetViewOrRedirect();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (User.Identity is {IsAuthenticated: true})
        {
            return RedirectToAction("Index", "Home");
        }

        var result = await _accountsCreator.AddAccountAsync(model);
        if (result.IsSuccess)
        {
            await _authorizer.SignIn(result.Data);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Message = result.Error;
        return View("Notification");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return GetViewOrRedirect();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _accountsProvider.GetAccountAsync(model);
        if (result.IsSuccess)
        {
            await _authorizer.SignIn(result.Data);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Message = Constants.ErrorMessages.InvalidAccount;
        return View("Notification");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _authorizer.SignOut();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Admins(int pageNumber = 1)
    {
        var result = await _accountsProvider.GetAdminsAsync(pageNumber);
        if (result.IsSuccess)
        {
            if (result.Data.List.Count > 0)
            {
                return View(result.Data);
            }

            ViewBag.Message = Constants.ErrorMessages.NoAdmins;
        }
        else
        {
            ViewBag.Message = result.Error;
        }

        return View("Notification");
    }

    [HttpGet]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Users(int pageNumber = 1)
    {
        var result = await _accountsProvider.GetUsersAsync(pageNumber);
        if (result.IsSuccess)
        {
            if (result.Data.List.Count > 0)
            {
                return View(result.Data);
            }

            ViewBag.Message = Constants.ErrorMessages.NoUsers;
        }
        else
        {
            ViewBag.Message = result.Error;
        }

        return View("Notification");
    }

    [HttpGet]
    [Authorize(Roles = Constants.Roles.SuperAdmin)]
    public async Task<IActionResult> MakeAdmin([FromRoute] string email)
    {
        var result = await _accountsUpdater.SetAdminRightsAsync(email);
        if (result.IsSuccess)
        {
            return RedirectToAction("Users", "Account");
        }

        ViewBag.Message = result.Error;
        return View("Notification");
    }

    [HttpGet]
    [Authorize(Roles = Constants.Roles.SuperAdmin)]
    public async Task<IActionResult> MakeUser([FromRoute] string email)
    {
        var result = await _accountsUpdater.RemoveAdminRightsAsync(email);
        if (result.IsSuccess)
        {
            return RedirectToAction("Admins", "Account");
        }

        ViewBag.Message = result.Error;
        return View("Notification");
    }

    private IActionResult GetViewOrRedirect()
    {
        if (User.Identity is {IsAuthenticated: true})
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }
}