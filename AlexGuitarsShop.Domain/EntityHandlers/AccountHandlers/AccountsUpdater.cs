using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;

namespace AlexGuitarsShop.Domain.EntityHandlers.AccountHandlers;

public class AccountsUpdater : IAccountsUpdater
{
    private readonly IUserRepository _userRepository;

    public AccountsUpdater(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task SetAdminRightsAsync(string email)
    {
        await _userRepository!.SetAdminRightsAsync(email)!;
    }

    public async Task RemoveAdminRightsAsync(string email)
    {
        await _userRepository!.RemoveAdminRightsAsync(email)!;
    }
}