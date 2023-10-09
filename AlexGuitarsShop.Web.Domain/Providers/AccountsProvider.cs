using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Account;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Providers;

public class AccountsProvider : IAccountsProvider
{
    private readonly IShopBackendService _shopBackendService;

    public AccountsProvider(IShopBackendService shopBackendService)
    {
        _shopBackendService = shopBackendService;
    }

    public async Task<IResultDto<AccountDto>> GetAccountAsync(LoginViewModel model)
    {
        if (model == null)
        {
            return ResultDtoCreator.GetInvalidResult<AccountDto>(Constants.Account.IncorrectAccount);
        }

        AccountDto accountDto = model.ToAccountDto();
        return await _shopBackendService.PostAsync(accountDto, Constants.Routes.Login);
    }

    public async Task<IResultDto<PaginatedListViewModel<AccountDto>>> GetAdminsAsync(int pageNumber)
    {
        var result = await _shopBackendService.GetAsync<PaginatedListDto<AccountDto>, int>(
            Constants.Routes.Admins, pageNumber);
        return result is {IsSuccess: true}
            ? ResultDtoCreator.GetValidResult(result.Data.ToPaginatedListViewModel(Title.Admins, pageNumber))
            : ResultDtoCreator.GetInvalidResult<PaginatedListViewModel<AccountDto>>(result!.Error);
    }

    public async Task<IResultDto<PaginatedListViewModel<AccountDto>>> GetUsersAsync(int pageNumber)
    {
        var result =
            await _shopBackendService.GetAsync<PaginatedListDto<AccountDto>, int>(Constants.Routes.Users, pageNumber);
        return result is {IsSuccess: true}
            ? ResultDtoCreator.GetValidResult(result.Data.ToPaginatedListViewModel(Title.Users, pageNumber))
            : ResultDtoCreator.GetInvalidResult<PaginatedListViewModel<AccountDto>>(result!.Error);
    }
}