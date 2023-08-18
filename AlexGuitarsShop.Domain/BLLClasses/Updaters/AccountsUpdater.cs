using AlexGuitarsShop.DAL;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;

namespace AlexGuitarsShop.Domain.BLLClasses.Updaters;

public class AccountsUpdater : IAccountsUpdater
{
    private readonly IAccountRepository _accountRepository;

    public AccountsUpdater(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task SetAdminRightsAsync(string email)
    {
        if (_accountRepository == null) throw new ArgumentNullException(nameof(_accountRepository));
        await _accountRepository.UpdateAsync(email, (int)Role.Admin)!;
    }

    public async Task RemoveAdminRightsAsync(string email)
    {
        if (_accountRepository == null) throw new ArgumentNullException(nameof(_accountRepository));
        await _accountRepository.UpdateAsync(email, (int)Role.User)!;
    }
}