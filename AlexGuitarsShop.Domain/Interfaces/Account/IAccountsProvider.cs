using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountsProvider
{
    Task<IResult<DAL.Models.Account>> GetAccountAsync(Login login);

    Task<IResult<List<Common.Models.Account>>> GetUsersAsync(int offset, int limit);

    Task<IResult<List<Common.Models.Account>>> GetAdminsAsync(int offset, int limit);

    Task<IResult<int>> GetUsersCountAsync();

    Task<IResult<int>> GetAdminsCountAsync();

    Task<IResult<int>> GetId(string email);
}