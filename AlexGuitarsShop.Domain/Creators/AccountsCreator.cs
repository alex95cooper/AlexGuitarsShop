using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Models;

namespace AlexGuitarsShop.Domain.Creators;

public class AccountsCreator : IAccountsCreator
{
    private const string ExistEmailErrorMessage = "User with this e-mail already exists";
    
    private readonly IAccountRepository _accountRepository;

    public AccountsCreator(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IResult<Account>> AddAccountAsync(Register register)
    {
        register = register ?? throw new ArgumentNullException(nameof(register));
        var account = await _accountRepository.FindAsync(register.Email);
        if (account != null)
        {
            return ResultCreator.GetInvalidResult<Account>(ExistEmailErrorMessage);
        }

        account = register.ToAccount();
        account.Password = PasswordHasher.HashPassword(account.Password);
        await _accountRepository.CreateAsync(account);
        return ResultCreator.GetValidResult(account);
    }
}