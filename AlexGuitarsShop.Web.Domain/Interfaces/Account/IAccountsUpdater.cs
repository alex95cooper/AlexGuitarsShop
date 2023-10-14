using AlexGuitarsShop.Common;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Account;

public interface IAccountsUpdater
{
    Task<IResultDto> SetAdminRightsAsync(string email);

    Task<IResultDto> RemoveAdminRightsAsync(string email);
}