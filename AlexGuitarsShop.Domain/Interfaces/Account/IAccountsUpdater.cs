using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountsUpdater
{
    Task<IResult<AccountDto>> SetAdminRightsAsync(string email);

    Task<IResult<AccountDto>> RemoveAdminRightsAsync(string email);
}