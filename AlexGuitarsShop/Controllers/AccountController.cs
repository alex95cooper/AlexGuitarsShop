using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Domain.Interfaces;
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
    private readonly IAuthorizer _authorizer;
    private readonly Paginator _paginator;
    
    public AccountController(IAccountsCreator accountsCreator, IAccountsProvider accountsProvider,
        IAccountsUpdater accountsUpdater, IAuthorizer authorizer)
    {
        _accountsCreator = accountsCreator ?? throw new ArgumentNullException(nameof(accountsCreator));
        _accountsProvider = accountsProvider ?? throw new ArgumentNullException(nameof(accountsProvider));
        _accountsUpdater = accountsUpdater ?? throw new ArgumentNullException(nameof(accountsUpdater));
        _authorizer = authorizer ?? throw new ArgumentNullException(nameof(authorizer));
        _paginator = new Paginator(Limit);
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var result = await _accountsCreator.AddAccountAsync(model)!;
        result = result ?? throw new ArgumentNullException(nameof(result));
        if (result.IsSuccess)
        {
            await _authorizer.SignIn(result.Data);
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("",
            result.Error ?? throw new ArgumentNullException(nameof(result.Error)));

        return View(model);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _accountsProvider.GetAccountAsync(model)!;
        result = result ?? throw new ArgumentNullException(nameof(result));
        if (result.IsSuccess)
        {
            await _authorizer.SignIn(result.Data);
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("",
            result.Error ?? throw new ArgumentNullException(nameof(result.Error)));

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _authorizer.SignOut();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Admins(int pageNumber = 1)
    {
        var countResult = await _accountsProvider.GetAdminsCountAsync()!;
        _paginator.SetPaginationValues(pageNumber, countResult);
        var result = await _accountsProvider.GetAdminsAsync(_paginator.OffSet, Limit)!;
        result = result ?? throw new ArgumentNullException(nameof(result));
        return View(_paginator.GetPaginatedList(
            result.Data, Title.Admins, pageNumber));
    }

    [HttpGet]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> Users(int pageNumber = 1)
    {
        var countResult = await _accountsProvider.GetUsersCountAsync()!;
        _paginator.SetPaginationValues(pageNumber, countResult);
        var result = await _accountsProvider.GetUsersAsync(_paginator.OffSet, Limit)!;
        result = result ?? throw new ArgumentNullException(nameof(result));
        return View(_paginator.GetPaginatedList(
            result.Data, Title.Users, pageNumber));
    }

    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> MakeAdmin(string email)
    {
        await _accountsUpdater.SetAdminRightsAsync(email)!;
        return RedirectToAction("Users");
    }

    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> MakeUser(string email)
    {
        await _accountsUpdater.RemoveAdminRightsAsync(email)!;
        return RedirectToAction("Admins");
    }
}