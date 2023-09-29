using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountsProvider
{
    Task<IResult<AccountDto>> GetAccountAsync(AccountDto accountDto);

    Task<IResult<List<AccountDto>>> GetUsersAsync(int offset, int limit);

    Task<IResult<List<AccountDto>>> GetAdminsAsync(int offset, int limit);

    Task<IResult<int>> GetUsersCountAsync();

    Task<IResult<int>> GetAdminsCountAsync();

    Task<IResult<int>> GetId(string email);
}