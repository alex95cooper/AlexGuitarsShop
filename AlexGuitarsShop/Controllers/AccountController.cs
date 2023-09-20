using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Validators;
using Microsoft.AspNetCore.Mvc;
using AccountDto = AlexGuitarsShop.Common.Models.Account;

namespace AlexGuitarsShop.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IAccountsCreator _accountsCreator;
    private readonly IAccountsProvider _accountsProvider;
    private readonly IAccountsUpdater _accountsUpdater;
    private readonly IAccountValidator _accountValidator;

    public AccountController(IAccountsCreator accountsCreator, IAccountsProvider accountsProvider,
        IAccountsUpdater accountsUpdater, IAccountValidator accountValidator)
    {
        _accountsCreator = accountsCreator;
        _accountsProvider = accountsProvider;
        _accountsUpdater = accountsUpdater;
        _accountValidator = accountValidator;
    }

    [HttpPost("Register")]
    public async Task<IResult<AccountDto>> Register(Register register)
    {
        if (register == null || !_accountValidator.CheckIfRegisterIsValid(register))
        {
            return ResultCreator.GetInvalidResult<AccountDto>(
                Constants.ErrorMessages.InvalidAccount);
        }

        var result = await _accountsCreator.AddAccountAsync(register);
        return result.IsSuccess
            ? ResultCreator.GetValidResult(result.Data.ToAccount())
            : ResultCreator.GetInvalidResult<AccountDto>(result.Error);
    }
    
    [HttpPost("Login")]
    public async Task<IResult<AccountDto>> Login(Login login)
    {
        if (login == null || !_accountValidator.CheckIfLoginIsValid(login))
        {
            return ResultCreator.GetInvalidResult<AccountDto>(
                Constants.ErrorMessages.InvalidAccount);
        }

        var result = await _accountsProvider.GetAccountAsync(login);
        return result.IsSuccess
            ? ResultCreator.GetValidResult(result.Data.ToAccount())
            : ResultCreator.GetInvalidResult<AccountDto>(result.Error);
    }

    [HttpGet("Admins/{pageNumber}")]
    public async Task<IResult<PaginatedList<AccountDto>>> Admins(int pageNumber = 1)
    {
        int count = (await _accountsProvider.GetAdminsCountAsync()).Data;
        if (count == 0)
        {
            return ResultCreator.GetInvalidResult<PaginatedList<AccountDto>>(
                Constants.ErrorMessages.NoAdmins);
        }
        
        if (!PageValidator.CheckIfPageIsValid(pageNumber, Paginator.Limit, count))
        {
            return ResultCreator.GetInvalidResult<PaginatedList<AccountDto>>(
                Constants.ErrorMessages.InvalidPage);
        }
        
        int offset = Paginator.GetOffset(pageNumber);
        List<AccountDto> list = (await _accountsProvider.GetAdminsAsync(offset, Paginator.Limit)).Data;
        return ResultCreator.GetValidResult(
            new PaginatedList<AccountDto>
            {
                CountOfAll = count,
                LimitedList = list
            });
    }

    [HttpGet("Users/{pageNumber}")]
    public async Task<IResult<PaginatedList<AccountDto>>> Users(int pageNumber = 1)
    {
        int count = (await _accountsProvider.GetUsersCountAsync()).Data;
        if (count == 0)
        {
            return ResultCreator.GetInvalidResult<PaginatedList<AccountDto>>(
                Constants.ErrorMessages.NoUsers);
        }
        
        if (!PageValidator.CheckIfPageIsValid(pageNumber, Paginator.Limit, count))
        {
            return ResultCreator.GetInvalidResult<PaginatedList<AccountDto>>(
                Constants.ErrorMessages.InvalidPage);
        }

        int offset = Paginator.GetOffset(pageNumber);
        List<AccountDto> list = (await _accountsProvider.GetUsersAsync(offset, Paginator.Limit)).Data;
        return ResultCreator.GetValidResult(
            new PaginatedList<AccountDto>
            {
                CountOfAll = count,
                LimitedList = list
            });
    }

    [HttpGet("MakeAdmin/{email}")]
    public async Task<IResult<string>> MakeAdmin(string email)
    {
        if (!await _accountValidator.CheckIfEmailExist(email))
        {
            return ResultCreator.GetInvalidResult<string>(
                Constants.ErrorMessages.InvalidEmail);
        }

        await _accountsUpdater.SetAdminRightsAsync(email);
        return ResultCreator.GetValidResult(email);
    }

    [HttpGet("MakeUser/{email}")]
    public async Task<IResult<string>> MakeUser(string email)
    {
        if (!await _accountValidator.CheckIfEmailExist(email))
        {
            return ResultCreator.GetInvalidResult<string>(
                Constants.ErrorMessages.InvalidEmail);
        }

        await _accountsUpdater.RemoveAdminRightsAsync(email);
        return ResultCreator.GetValidResult(email);
    }
}