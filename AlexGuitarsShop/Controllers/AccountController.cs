using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Validators;
using AlexGuitarsShop.Extensions;
using AlexGuitarsShop.ViewModels;

namespace AlexGuitarsShop.Controllers;

public class AccountController : Controller
{
    private readonly IAccountsCreator _accountsCreator;
    private readonly IAccountsProvider _accountsProvider;
    private readonly IAccountsUpdater _accountsUpdater;
    private readonly IAccountValidator _accountValidator;
    private readonly IAuthorizer _authorizer;

    public AccountController(IAccountsCreator accountsCreator, IAccountsProvider accountsProvider,
        IAccountsUpdater accountsUpdater, IAccountValidator accountValidator, IAuthorizer authorizer)
    {
        _accountsCreator = accountsCreator;
        _accountsProvider = accountsProvider;
        _accountsUpdater = accountsUpdater;
        _accountValidator = accountValidator;
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
        if (model == null || !_accountValidator.CheckIfRegisterIsValid(model.ToRegister()))
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidAccount;
            return View("Notification");
        }

        if (User.Identity is {IsAuthenticated: true})
        {
            return RedirectToAction("Index", "Home");
        }

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
    public IActionResult Login()
    {
        return GetViewOrRedirect();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (model == null || !_accountValidator.CheckIfLoginIsValid(model.ToLogin()))
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidAccount;
            return View("Notification");
        }

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
        int count = (await _accountsProvider.GetAdminsCountAsync()).Data;
        if (!PageValidator.CheckIfPageIsValid(pageNumber, Paginator.Limit, count))
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidPage;
            return View("Notification");
        }

        int offset = Paginator.GetOffset(pageNumber);
        List<Account> list = (await _accountsProvider.GetAdminsAsync(offset, Paginator.Limit)).Data;
        PaginatedListViewModel<Account> model = list.ToPaginatedList(Title.Admins, count, pageNumber);
        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Users(int pageNumber = 1)
    {
        int count = (await _accountsProvider.GetUsersCountAsync()).Data;
        if (!PageValidator.CheckIfPageIsValid(pageNumber, Paginator.Limit, count))
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidPage;
            return View("Notification");
        }

        int offset = Paginator.GetOffset(pageNumber);
        List<Account> list = (await _accountsProvider.GetUsersAsync(offset, Paginator.Limit)).Data;
        PaginatedListViewModel<Account> model = list.ToPaginatedList(Title.Users, count, pageNumber);
        return View(model);
    }

    [Authorize(Roles = Constants.Roles.SuperAdmin)]
    public async Task<IActionResult> MakeAdmin(string email)
    {
        if (!await _accountValidator.CheckIfEmailExist(email))
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidEmail;
            return View("Notification");
        }

        await _accountsUpdater.SetAdminRightsAsync(email);
        return RedirectToAction("Users", "Account");
    }

    [Authorize(Roles = Constants.Roles.SuperAdmin)]
    public async Task<IActionResult> MakeUser(string email)
    {
        if (!await _accountValidator.CheckIfEmailExist(email))
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidEmail;
            return View("Notification");
        }

        await _accountsUpdater.RemoveAdminRightsAsync(email);
        return RedirectToAction("Admins", "Account");
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