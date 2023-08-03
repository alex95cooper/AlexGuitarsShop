using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.BLLClasses.Providers;

public class AccountsProvider : IAccountsProvider
{
    private readonly IUserRepository _userRepository;

    public AccountsProvider(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IResult<User>> GetAccountAsync(LoginViewModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        if (_userRepository == null) throw new ArgumentNullException(nameof(_userRepository));
        var user = await _userRepository.FindAsync(model.Email)!;
        if (user == null || user.Password != PasswordHasher.HashPassword(model.Password))
        {
            string message = user == null ? "User is not found" : "Invalid password or login";
            return ResultCreator.GetInvalidResult<User>(message);
        }

        return ResultCreator.GetValidResult(user);
    }

    public async Task<IResult<List<User>>> GetUsersAsync(int offset, int limit)
    {
        if (_userRepository == null) throw new ArgumentNullException(nameof(_userRepository));
        var userList = await _userRepository.GetUsersAsync(offset, limit)!;
        return ResultCreator.GetValidResult(userList);
    }

    public async Task<IResult<List<User>>> GetAdminsAsync(int offset, int limit)
    {
        if (_userRepository == null) throw new ArgumentNullException(nameof(_userRepository));
        var userList = await _userRepository.GetAdminsAsync(offset, limit)!;
        return ResultCreator.GetValidResult(userList);
    }

    public async Task<IResult<int>> GetUsersCountAsync()
    {
        if (_userRepository == null) throw new ArgumentNullException(nameof(_userRepository));
        int usersCount = await _userRepository.GetUsersCountAsync()!;
        return ResultCreator.GetValidResult(usersCount);
    }

    public async Task<IResult<int>> GetAdminsCountAsync()
    {
        if (_userRepository == null) throw new ArgumentNullException(nameof(_userRepository));
        int adminsCount = await _userRepository.GetAdminsCountAsync()!;
        return ResultCreator.GetValidResult(adminsCount);
    }
}