namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountsUpdater
{
    Task SetAdminRightsAsync(string email);

    Task RemoveAdminRightsAsync(string email);
}