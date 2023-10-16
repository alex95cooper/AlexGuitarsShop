namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountsUpdater
{
    Task<IResult> SetAdminRightsAsync(string email);

    Task<IResult> RemoveAdminRightsAsync(string email);
}