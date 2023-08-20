using AlexGuitarsShop.Domain.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountsProvider
{
    Task<IResult<DAL.Models.Account>> GetAccountAsync(Login login);

    Task<IResult<List<DAL.Models.Account>>> GetUsersAsync(int offset, int limit);

    Task<IResult<List<DAL.Models.Account>>> GetAdminsAsync(int offset, int limit);

    Task<IResult<int>> GetUsersCountAsync();

    Task<IResult<int>> GetAdminsCountAsync();
}