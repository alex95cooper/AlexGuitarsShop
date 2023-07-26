using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.EntityHandlers.AccountHandlers;

public class AccountsProvider : IAccountsProvider
{
    private readonly IUserRepository _userRepository;

    public AccountsProvider(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IResponse<User>> GetAccountAsync(LoginViewModel model)
    {
        var user = await _userRepository!.GetUserByEmailAsync(model!.Email)!;
        if (user == null || user.Password != PasswordHasher.HashPassword(model.Password))
        {
            string message = user == null ? "User is not found" : "Invalid password or login";
            return GetInvalidResponse<User>(message);
        }

        return new Response<User>
        {
            StatusCode = StatusCode.Ok,
            Data = user
        };
    }

    public async Task<IResponse<List<User>>> GetUsersAsync(int offset, int limit)
    {
        var userList = await _userRepository!.GetUsersByLimitAsync(offset, limit)!;
        if (userList is {Count: 0})
        {
            return GetInvalidResponse<List<User>>("No users");
        }

        return new Response<List<User>>
        {
            Data = userList,
            StatusCode = StatusCode.Ok
        };
    }

    public async Task<IResponse<List<User>>> GetAdminsAsync(int offset, int limit)
    {
        var userList = await _userRepository!.GetAdminsByLimitAsync(offset, limit)!;
        if (userList is {Count: 0})
        {
            return GetInvalidResponse<List<User>>("No admins");
        }

        return new Response<List<User>>
        {
            Data = userList,
            StatusCode = StatusCode.Ok
        };
    }

    public async Task<IResponse<int>> GetUsersCountAsync()
    {
        return new Response<int>
        {
            Data = await _userRepository!.GetUsersCountAsync()!,
            StatusCode = StatusCode.Ok
        };
    }

    public async Task<IResponse<int>> GetAdminsCountAsync()
    {
        return new Response<int>
        {
            Data = await _userRepository!.GetAdminsCountAsync()!,
            StatusCode = StatusCode.Ok
        };
    }

    private IResponse<T> GetInvalidResponse<T>(string message)
    {
        return new Response<T>
        {
            StatusCode = StatusCode.InternalServerError,
            Description = message
        };
    }
}