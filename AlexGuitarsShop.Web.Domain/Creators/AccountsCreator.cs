using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Account;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Creators;

public class AccountsCreator : IAccountsCreator
{
    private readonly IShopBackendService _shopBackendService;

    public AccountsCreator(IShopBackendService shopBackendService)
    {
        _shopBackendService = shopBackendService;
    }

    public async Task<IResultDto<AccountDto>> AddAccountAsync(RegisterViewModel model)
    {
        if (model == null)
        {
            ResultDtoCreator.GetInvalidResult<AccountDto>(Constants.Account.IncorrectAccount);
        }

        AccountDto registerDto = model.ToAccountDto();
        return await _shopBackendService.PostAsync(registerDto, Constants.Routes.Register);
    }
}