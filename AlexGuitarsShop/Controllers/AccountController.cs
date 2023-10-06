using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Validators;
using AlexGuitarsShop.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

[ApiController]
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

    [HttpPost("accounts/register")]
    public async Task<ActionResult<ResultDto<AccountDto>>> Register([FromBody] AccountDto accountDto)
    {
        var validationResult = _accountValidator.CheckIfRegisterIsValid(accountDto);
        if (!validationResult.IsSuccess)
        {
            return _resultMaker.ResolveResult(validationResult);
        }

        var result = await _accountsCreator.AddAccountAsync(accountDto);
        return _resultMaker.ResolveResult(result);
    }

    [HttpPost("accounts/login")]
    public async Task<ActionResult<ResultDto<AccountDto>>> Login([FromBody] AccountDto accountDto)
    {
        var validationResult = _accountValidator.CheckIfLoginIsValid(accountDto);
        if (!validationResult.IsSuccess)
        {
            return _resultMaker.ResolveResult(validationResult);
        }

        var result = await _accountsProvider.GetAccountAsync(accountDto);
        return _resultMaker.ResolveResult(result);
    }

    [HttpGet("accounts/admins")]
    public async Task<ActionResult<ResultDto<PaginatedListDto<AccountDto>>>> Admins([FromQuery] int pageNumber = 1)
    {
        int count = (await _accountsProvider.GetAdminsCountAsync()).Data;
        if (count == 0)
        {
            return _resultMaker.ResolveResult(ResultCreator.GetValidResult(
                new PaginatedListDto<AccountDto> {CountOfAll = 0, LimitedList = new List<AccountDto>()},
                HttpStatusCode.NoContent));
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

    [HttpGet("accounts/users")]
    public async Task<ActionResult<ResultDto<PaginatedListDto<AccountDto>>>> Users([FromQuery] int pageNumber = 1)
    {
        int count = (await _accountsProvider.GetUsersCountAsync()).Data;
        if (count == 0)
        {
            return _resultMaker.ResolveResult(ResultCreator.GetValidResult(
                new PaginatedListDto<AccountDto> {CountOfAll = 0, LimitedList = new List<AccountDto>()},
                HttpStatusCode.NoContent));
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

    [HttpPut("accounts/make-admin")]
    public async Task<ActionResult<ResultDto<AccountDto>>> MakeAdmin([FromBody] AccountDto account)
    {
        var validationResult = await _accountValidator.CheckIfEmailExist(account.Email);
        if (!validationResult.IsSuccess)
        {
            return _resultMaker.ResolveResult(validationResult);
        }

        var result = await _accountsUpdater.SetAdminRightsAsync(account.Email);
        return _resultMaker.ResolveResult(result);
    }

    [HttpPut("accounts/make-user")]
    public async Task<ActionResult<ResultDto<AccountDto>>> MakeUser([FromBody] AccountDto account)
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