using System.Net;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.Account;

namespace AlexGuitarsShop.Domain.Providers;

public class AccountsProvider : IAccountsProvider
{
    private readonly IAccountRepository _accountRepository;

    public AccountsProvider(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IResult<AccountDto>> GetAccountAsync(AccountDto accountDto)
    {
        var account = await _accountRepository.FindAsync(accountDto.Email);
        if (account == null || account.Password != PasswordHasher.HashPassword(accountDto.Password))
        {
            string message = account == null ? "User is not found" : "Invalid password or login";
            return ResultCreator.GetInvalidResult<AccountDto>(message, HttpStatusCode.BadRequest);
        }

        return ResultCreator.GetValidResult(account.ToAccountDto(), HttpStatusCode.OK);
    }

    public async Task<IResult<List<AccountDto>>> GetUsersAsync(int offset, int limit)
    {
        var userList = await _accountRepository.GetUsersAsync(offset, limit)!;
        return GetValidResult(userList);
    }

    public async Task<IResult<List<AccountDto>>> GetAdminsAsync(int offset, int limit)
    {
        var userList = await _accountRepository.GetAdminsAsync(offset, limit);
        return GetValidResult(userList);
    }

    public async Task<IResult<int>> GetUsersCountAsync()
    {
        int usersCount = await _accountRepository.GetUsersCountAsync();
        return ResultCreator.GetValidResult(usersCount, HttpStatusCode.OK);
    }

    public async Task<IResult<int>> GetAdminsCountAsync()
    {
        int adminsCount = await _accountRepository.GetAdminsCountAsync();
        return ResultCreator.GetValidResult(adminsCount, HttpStatusCode.OK);
    }

    public async Task<IResult<int>> GetId(string email)
    {
        var account = await _accountRepository.FindAsync(email);
        return ResultCreator.GetValidResult(account.Id, HttpStatusCode.OK);
    }

    private static IResult<List<AccountDto>> GetValidResult(IEnumerable<Account> userList)
    {
        var userListDto = ListMapper.ToDtoAccountList(userList);
        return userListDto.Count == 0
            ? ResultCreator.GetValidResult(userListDto, HttpStatusCode.NoContent)
            : ResultCreator.GetValidResult(userListDto, HttpStatusCode.OK);
    }
}