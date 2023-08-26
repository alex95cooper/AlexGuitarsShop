using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Models;

namespace AlexGuitarsShop.Domain.Providers;

public class AccountsProvider : IAccountsProvider
{
    private readonly IAccountRepository _accountRepository;

    public AccountsProvider(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IResult<Account>> GetAccountAsync(Login login)
    {
        login = login ?? throw new ArgumentNullException(nameof(login));
        var account = await _accountRepository.FindAsync(login.Email);
        if (account == null || account.Password != PasswordHasher.HashPassword(login.Password))
        {
            string message = account == null ? "User is not found" : "Invalid password or login";
            return ResultCreator.GetInvalidResult<Account>(message);
        }

        return ResultCreator.GetValidResult(account);
    }

    public async Task<IResult<List<Account>>> GetUsersAsync(int offset, int limit)
    {
        var userList = await _accountRepository.GetUsersAsync(offset, limit)!;
        return ResultCreator.GetValidResult(userList);
    }

    public async Task<IResult<List<Account>>> GetAdminsAsync(int offset, int limit)
    {
        var userList = await _accountRepository.GetAdminsAsync(offset, limit);
        return ResultCreator.GetValidResult(userList);
    }

    public async Task<IResult<int>> GetUsersCountAsync()
    {
        int usersCount = await _accountRepository.GetUsersCountAsync();
        return ResultCreator.GetValidResult(usersCount);
    }

    public async Task<IResult<int>> GetAdminsCountAsync()
    {
        int adminsCount = await _accountRepository.GetAdminsCountAsync();
        return ResultCreator.GetValidResult(adminsCount);
    }
    
    public async Task<IResult<string>> GetCartId(string email)
    {
        var account = await _accountRepository.FindAsync(email);
        return ResultCreator.GetValidResult(account.CartId);
    }
}