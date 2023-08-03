using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain;
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
    private readonly ValidUserAuthorizer _validUserAuthorizer;

    private int _pageCount;
    private int _offset;

    public AccountController(IAccountsCreator accountsCreator,
        IAccountsProvider accountsProvider, IAccountsUpdater accountsUpdater)
    {
        _accountsCreator = accountsCreator;
        _accountsProvider = accountsProvider;
        _accountsUpdater = accountsUpdater;
        _validUserAuthorizer = new ValidUserAuthorizer(HttpContext);
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (_accountsCreator == null) throw new ArgumentNullException(nameof(_accountsCreator));
        if (_validUserAuthorizer == null) throw new ArgumentNullException(nameof(_validUserAuthorizer));
        var result = await _accountsCreator.AddAccountAsync(model)!;
        result = result ?? throw new ArgumentNullException(nameof(result));
        if (result.IsSuccess)
        {
            await _validUserAuthorizer.SignIn(result.Data);
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("",
            result.Description
            ?? throw new ArgumentNullException(nameof(result.Description)));

        return View(model);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (_accountsProvider == null) throw new ArgumentNullException(nameof(_accountsProvider));
        if (_validUserAuthorizer == null) throw new ArgumentNullException(nameof(_validUserAuthorizer));
        var result = await _accountsProvider.GetAccountAsync(model)!;
        result = result ?? throw new ArgumentNullException(nameof(result));
        if (result.IsSuccess)
        {
            await _validUserAuthorizer.SignIn(result.Data);
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("",
            result.Description
            ?? throw new ArgumentNullException(nameof(result.Description)));

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        if (_validUserAuthorizer == null) throw new ArgumentNullException(nameof(_validUserAuthorizer));
        await _validUserAuthorizer.SignOut();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Admins(int pageNumber = 1)
    {
        if (_accountsProvider == null) throw new ArgumentNullException(nameof(_accountsProvider));
        _offset = (pageNumber - 1) * Limit;
        var countResult = await _accountsProvider.GetAdminsCountAsync()!;
        countResult = countResult ?? throw new ArgumentNullException(nameof(countResult));
        int count = countResult.Data;
        _pageCount = count % Limit == 0 ? count / Limit : count / Limit + 1;
        var result = await _accountsProvider.GetAdminsAsync(_offset, Limit)!;
        result = result ?? throw new ArgumentNullException(nameof(result));
        ListViewModel<User> model = new(result.Data, Title.Admins, _pageCount, pageNumber);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Users(int pageNumber = 1)
    {
        if (_accountsProvider == null) throw new ArgumentNullException(nameof(_accountsProvider));
        _offset = (pageNumber - 1) * Limit;
        var countResult = await _accountsProvider.GetUsersCountAsync()!;
        countResult = countResult ?? throw new ArgumentNullException(nameof(countResult));
        int count = countResult.Data;
        _pageCount = count % Limit == 0 ? count / Limit : count / Limit + 1;
        var result = await _accountsProvider.GetUsersAsync(_offset, Limit)!;
        result = result ?? throw new ArgumentNullException(nameof(result));
        ListViewModel<User> model = new(result.Data, Title.Users, _pageCount, pageNumber);
        return View(model);
    }

    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> MakeAdmin(string email)
    {
        if (_accountsUpdater == null) throw new ArgumentNullException(nameof(_accountsUpdater));
        await _accountsUpdater.SetAdminRightsAsync(email)!;
        return RedirectToAction("Users", new {role = "Users"});
    }

    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> MakeUser(string email)
    {
        if (_accountsUpdater == null) throw new ArgumentNullException(nameof(_accountsUpdater));
        await _accountsUpdater.RemoveAdminRightsAsync(email)!;
        return RedirectToAction("Users", new {role = "Admins"});
    }
}