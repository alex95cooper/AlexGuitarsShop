using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Interfaces.Account;

namespace AlexGuitarsShop.Web.Domain.Updaters;

public class AccountsUpdater : IAccountsUpdater
{
    private readonly IShopBackendService _shopBackendService;

    public AccountsUpdater(IShopBackendService shopBackendService)
    {
        _shopBackendService = shopBackendService;
    }

    public async Task<IResultDto<AccountDto>> SetAdminRightsAsync(string email)
    {
        AccountDto accountDto = new AccountDto {Email = email};
        return await _shopBackendService.PutAsync(accountDto, Constants.Routes.MakeAdmin);
    }

    public async Task<IResultDto<AccountDto>> RemoveAdminRightsAsync(string email)
    {
        AccountDto accountDto = new AccountDto {Email = email};
        return await _shopBackendService.PutAsync(accountDto, Constants.Routes.MakeUser);
    }
}