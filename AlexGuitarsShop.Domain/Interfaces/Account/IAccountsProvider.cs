using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountsProvider
{
    Task<IResult<User>> GetAccountAsync(LoginViewModel model);

    Task<IResult<List<User>>> GetUsersAsync(int offset, int limit);

    Task<IResult<List<User>>> GetAdminsAsync(int offset, int limit);

    Task<IResult<int>> GetUsersCountAsync();

    Task<IResult<int>> GetAdminsCountAsync();
}