using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Providers;

public class GuitarsProvider : IGuitarsProvider
{
    private readonly IShopBackendService _shopBackendService;

    public GuitarsProvider(IShopBackendService shopBackendService)
    {
        _shopBackendService = shopBackendService;
    }

    public async Task<IResultDto<PaginatedListViewModel<GuitarDto>>> GetGuitarsByLimitAsync(int pageNumber)
    {
        var result = await _shopBackendService.GetAsync<PaginatedListDto<GuitarDto>, int>(
            Constants.Routes.GetGuitars, pageNumber);
        return result is {IsSuccess: true}
            ? ResultDtoCreator.GetValidResult(result.Data.ToPaginatedListViewModel(Title.Catalog, pageNumber))
            : ResultDtoCreator.GetInvalidResult<PaginatedListViewModel<GuitarDto>>(result!.Error);
    }

    public async Task<IResultDto<GuitarViewModel>> GetGuitarAsync(int id)
    {
        var result = await _shopBackendService.GetAsync<GuitarDto, int>(Constants.Routes.GetGuitar, id);
        return result is {IsSuccess: true}
            ? ResultDtoCreator.GetValidResult(result.Data.ToGuitarViewModel())
            : ResultDtoCreator.GetInvalidResult<GuitarViewModel>(result!.Error);
    }
}