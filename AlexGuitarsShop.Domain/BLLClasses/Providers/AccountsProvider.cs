using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.BLLClasses.Providers;

public class AccountsProvider : IAccountsProvider
{
    private readonly IAccountRepository _accountRepository;

    public AccountsProvider(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IResult<Account>> GetAccountAsync(LoginViewModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        if (_accountRepository == null) throw new ArgumentNullException(nameof(_accountRepository));
        var user = await _accountRepository.FindAsync(model.Email)!;
        if (user == null || user.Password != PasswordHasher.HashPassword(model.Password))
        {
            string message = user == null ? "User is not found" : "Invalid password or login";
            return ResultCreator.GetInvalidResult<Account>(message);
        }

        return ResultCreator.GetValidResult(user);
    }

    public async Task<IResult<List<Account>>> GetUsersAsync(int offset, int limit)
    {
        if (_accountRepository == null) throw new ArgumentNullException(nameof(_accountRepository));
        var userList = await _accountRepository.GetUsersAsync(offset, limit)!;
        return ResultCreator.GetValidResult(userList);
    }

    public async Task<IResult<List<Account>>> GetAdminsAsync(int offset, int limit)
    {
        if (_accountRepository == null) throw new ArgumentNullException(nameof(_accountRepository));
        var userList = await _accountRepository.GetAdminsAsync(offset, limit)!;
        return ResultCreator.GetValidResult(userList);
    }

    public async Task<IResult<int>> GetUsersCountAsync()
    {
        if (_accountRepository == null) throw new ArgumentNullException(nameof(_accountRepository));
        int usersCount = await _accountRepository.GetUsersCountAsync()!;
        return ResultCreator.GetValidResult(usersCount);
    }

    public async Task<IResult<int>> GetAdminsCountAsync()
    {
        if (_accountRepository == null) throw new ArgumentNullException(nameof(_accountRepository));
        int adminsCount = await _accountRepository.GetAdminsCountAsync()!;
        return ResultCreator.GetValidResult(adminsCount);
    }
}