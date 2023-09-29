using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Validators;
using AlexGuitarsShop.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IAccountsCreator _accountsCreator;
    private readonly IAccountsProvider _accountsProvider;
    private readonly IAccountsUpdater _accountsUpdater;
    private readonly IAccountValidator _accountValidator;
    private readonly ActionResultMaker _resultMaker;

    public AccountController(IAccountsCreator accountsCreator, IAccountsProvider accountsProvider,
        IAccountsUpdater accountsUpdater, IAccountValidator accountValidator)
    {
        _accountsCreator = accountsCreator;
        _accountsProvider = accountsProvider;
        _accountsUpdater = accountsUpdater;
        _accountValidator = accountValidator;
        _resultMaker = new ActionResultMaker();
    }

    [HttpPost(Constants.Routes.Register)]
    public async Task<ActionResult<Result<AccountDto>>> Register(AccountDto accountDto)
    {
        var validationResult = _accountValidator.CheckIfRegisterIsValid(accountDto);
        if (!validationResult.IsSuccess)
        {
            return _resultMaker.ResolveResult(validationResult);
        }

        var result = await _accountsCreator.AddAccountAsync(accountDto);
        return _resultMaker.ResolveResult(result);
    }
    
    [HttpPost(Constants.Routes.Login)]
    public async Task<ActionResult<Result<AccountDto>>> Login(AccountDto accountDto)
    {
        var validationResult = _accountValidator.CheckIfLoginIsValid(accountDto);
        if (!validationResult.IsSuccess)
        {
            return _resultMaker.ResolveResult(validationResult);
        }

        var result = await _accountsProvider.GetAccountAsync(accountDto);
        return _resultMaker.ResolveResult(result);
    }

    [HttpGet(Constants.Routes.Admins)]
    public async Task<ActionResult<Result<PaginatedListDto<AccountDto>>>> Admins(int pageNumber = 1)
    {
        int count = (await _accountsProvider.GetAdminsCountAsync()).Data;
        if (count == 0)
        {
            return _resultMaker.ResolveResult(ResultCreator.GetValidResult(
                new PaginatedListDto<AccountDto>(), HttpStatusCode.NoContent));
        }

        if (!PageValidator.CheckIfPageIsValid(pageNumber, Paginator.Limit, count))
        {
            return _resultMaker.ResolveResult(ResultCreator.GetInvalidResult<PaginatedListDto<AccountDto>>(
                Constants.ErrorMessages.InvalidPage, HttpStatusCode.BadRequest));
        }

        int offset = Paginator.GetOffset(pageNumber);
        List<AccountDto> list = (await _accountsProvider.GetAdminsAsync(offset, Paginator.Limit)).Data;
        return _resultMaker.ResolveResult(ResultCreator.GetValidResult(
            new PaginatedListDto<AccountDto> {CountOfAll = count, LimitedList = list}, HttpStatusCode.OK));
    }

    [HttpGet(Constants.Routes.Users)]
    public async Task<ActionResult<Result<PaginatedListDto<AccountDto>>>> Users(int pageNumber = 1)
    {
        int count = (await _accountsProvider.GetUsersCountAsync()).Data;
        if (count == 0)
        {
            return _resultMaker.ResolveResult(ResultCreator.GetValidResult(
                new PaginatedListDto<AccountDto>(), HttpStatusCode.NoContent));
        }

        if (!PageValidator.CheckIfPageIsValid(pageNumber, Paginator.Limit, count))
        {
            return _resultMaker.ResolveResult(ResultCreator.GetInvalidResult<PaginatedListDto<AccountDto>>(
                Constants.ErrorMessages.InvalidPage, HttpStatusCode.BadRequest));
        }

        int offset = Paginator.GetOffset(pageNumber);
        List<AccountDto> list = (await _accountsProvider.GetUsersAsync(offset, Paginator.Limit)).Data;
        return _resultMaker.ResolveResult(ResultCreator.GetValidResult(
            new PaginatedListDto<AccountDto> {CountOfAll = count, LimitedList = list}, HttpStatusCode.OK));
    }

    [HttpPut(Constants.Routes.MakeAdmin)]
    public async Task<ActionResult<Result<AccountDto>>> MakeAdmin(AccountDto account)
    {
        var validationResult = await _accountValidator.CheckIfEmailExist(account.Email);
        if (!validationResult.IsSuccess)
        {
            return _resultMaker.ResolveResult(validationResult);
        }

        var result = await _accountsUpdater.SetAdminRightsAsync(account.Email);
        return _resultMaker.ResolveResult(result);
    }

    [HttpPut(Constants.Routes.MakeUser)]
    public async Task<ActionResult<Result<AccountDto>>> MakeUser(AccountDto account)
    {
        var validationResult = await _accountValidator.CheckIfEmailExist(account.Email);
        if (!validationResult.IsSuccess)
        {
            return _resultMaker.ResolveResult(validationResult);
        }

        var result = await _accountsUpdater.RemoveAdminRightsAsync(account.Email);
        return _resultMaker.ResolveResult(result);
    }
}