using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.BLLClasses.Creators;

public class AccountsCreator : IAccountsCreator
{
    private const string ExistEmailErrorMessage = "User with this e-mail already exists";
    
        
    private readonly IAccountRepository _accountRepository;

    public AccountsCreator(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IResult<Account>> AddAccountAsync(RegisterViewModel model)
    {
        if (_accountRepository == null) throw new ArgumentNullException(nameof(_accountRepository));
        if (model == null) throw new ArgumentNullException(nameof(model));
        var user = await _accountRepository.FindAsync(model.Email)!;
        if (user != null)
        {
            return ResultCreator.GetInvalidResult<Account>(ExistEmailErrorMessage);
        }

        user = model.ToUser();
        await _accountRepository.CreateAsync(user)!;
        return ResultCreator.GetValidResult(user);
    }
    
}