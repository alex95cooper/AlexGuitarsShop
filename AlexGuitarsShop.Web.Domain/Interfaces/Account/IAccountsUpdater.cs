using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Account;

public interface IAccountsUpdater
{
    Task<IResultDto<AccountDto>> SetAdminRightsAsync(string email);

    Task<IResultDto<AccountDto>> RemoveAdminRightsAsync(string email);
}