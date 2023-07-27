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
            return ResponseCreator.GetInvalidResponse<User>(message);
        }

        return ResponseCreator.GetValidResponse(user);
    }

    public async Task<IResponse<List<User>>> GetUsersAsync(int offset, int limit)
    {
        var userList = await _userRepository!.GetUsersByLimitAsync(offset, limit)!;
        return userList is {Count: 0} 
            ? ResponseCreator.GetInvalidResponse<List<User>>("No users") 
            : ResponseCreator.GetValidResponse(userList);
    }

    public async Task<IResponse<List<User>>> GetAdminsAsync(int offset, int limit)
    {
        var userList = await _userRepository!.GetAdminsByLimitAsync(offset, limit)!;
        return userList is {Count: 0} 
            ? ResponseCreator.GetInvalidResponse<List<User>>("No admins") 
            : ResponseCreator.GetValidResponse(userList);
    }

    public async Task<IResponse<int>> GetUsersCountAsync()
    {
        int usersCount = await _userRepository!.GetUsersCountAsync()!;
        return ResponseCreator.GetValidResponse(usersCount);
    }

    public async Task<IResponse<int>> GetAdminsCountAsync()
    {
        int adminsCount = await _userRepository!.GetAdminsCountAsync()!;
        return ResponseCreator.GetValidResponse(adminsCount);
    }
}