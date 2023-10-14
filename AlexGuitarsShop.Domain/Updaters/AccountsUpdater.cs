using AlexGuitarsShop.Common;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;

namespace AlexGuitarsShop.Domain.Updaters;

public class AccountsUpdater : IAccountsUpdater
{
    private readonly IAccountRepository _accountRepository;

    public AccountsUpdater(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IResult> SetAdminRightsAsync(string email)
    {
        await _accountRepository.UpdateAsync(email, (int) Role.Admin);
        return ResultCreator.GetValidResult();
    }

    public async Task<IResult> RemoveAdminRightsAsync(string email)
    {
        await _accountRepository.UpdateAsync(email, (int) Role.User);
        return ResultCreator.GetValidResult();
    }
}