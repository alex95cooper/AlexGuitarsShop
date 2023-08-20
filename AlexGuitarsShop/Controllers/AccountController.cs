using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Extensions;
using AlexGuitarsShop.ViewModels;

namespace AlexGuitarsShop.Controllers;

public class AccountController : Controller
{
    private const int Limit = 10;

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
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var result = await _accountsCreator.AddAccountAsync(model.ToRegister());
        if (result.IsSuccess)
        {
            await _authorizer.SignIn(result.Data);
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", result.Error);
        return View(model);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _accountsProvider.GetAccountAsync(model.ToLogin());
        if (result.IsSuccess)
        {
            await _authorizer.SignIn(result.Data);
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", result.Error);
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _authorizer.SignOut();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Admins(int pageNumber = 1)
    {
        int offset = (pageNumber - 1) * Limit;
        int count = (await _accountsProvider.GetAdminsCountAsync()).Data;
        List<Account> list = (await _accountsProvider.GetAdminsAsync(offset, Limit)).Data;
        ListViewModel<Account> model = new ListViewModel<Account>
        {
            List = list, Title = Title.Admins,
            TotalCount = count, CurrentPage = pageNumber
        };

        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Users(int pageNumber = 1)
    {
        int offset = (pageNumber - 1) * Limit;
        int count = (await _accountsProvider.GetUsersCountAsync()).Data;
        List<Account> list = (await _accountsProvider.GetUsersAsync(offset, Limit)).Data;
        ListViewModel<Account> model = new ListViewModel<Account>
        {
            List = list, Title = Title.Users,
            TotalCount = count, CurrentPage = pageNumber
        };

        return View(model);
    }

    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> MakeAdmin(string email)
    {
        await _accountsUpdater.SetAdminRightsAsync(email);
        return RedirectToAction("Users");
    }

    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> MakeUser(string email)
    {
        await _accountsUpdater.RemoveAdminRightsAsync(email);
        return RedirectToAction("Admins");
    }
}