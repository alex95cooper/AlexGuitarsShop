using AlexGuitarsShop.DAL;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;

namespace AlexGuitarsShop.Domain.BLLClasses.Updaters;

public class AccountsUpdater : IAccountsUpdater
{
    private readonly IUserRepository _userRepository;

    public AccountsUpdater(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task SetAdminRightsAsync(string email)
    {
        if (_userRepository == null) throw new ArgumentNullException(nameof(_userRepository));
        await _userRepository.UpdateAsync(email, (int)Role.Admin)!;
    }

    public async Task RemoveAdminRightsAsync(string email)
    {
        if (_userRepository == null) throw new ArgumentNullException(nameof(_userRepository));
        await _userRepository.UpdateAsync(email, (int)Role.User)!;
    }
}