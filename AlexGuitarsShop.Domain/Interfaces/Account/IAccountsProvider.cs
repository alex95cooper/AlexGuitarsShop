using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountsProvider
{
    Task<IResponse<User>> GetAccountAsync(LoginViewModel model);

    Task<IResponse<List<User>>> GetUsersAsync(int offset, int limit);

    Task<IResponse<List<User>>> GetAdminsAsync(int offset, int limit);

    Task<IResponse<int>> GetUsersCountAsync();

    Task<IResponse<int>> GetAdminsCountAsync();
}