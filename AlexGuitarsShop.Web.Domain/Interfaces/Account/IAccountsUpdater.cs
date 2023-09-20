using AlexGuitarsShop.Common;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Account;

public interface IAccountsUpdater
{
    Task<IResult<string>> SetAdminRightsAsync(string email);

    Task<IResult<string>> RemoveAdminRightsAsync(string email);
}