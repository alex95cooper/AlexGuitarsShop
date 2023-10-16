using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Creators;

public class GuitarsCreator : IGuitarsCreator
{
    private readonly IShopBackendService _shopBackendService;

    public GuitarsCreator(IShopBackendService shopBackendService)
    {
        _shopBackendService = shopBackendService;
    }

    public async Task<IResultDto> AddGuitarAsync(GuitarViewModel model)
    {
        if (model == null)
        {
            return ResultDtoCreator.GetInvalidResult<GuitarDto>(Constants.Guitar.IncorrectGuitar);
        }

        GuitarDto guitarDto = model.ToGuitarDto();
        guitarDto.Image = model!.Avatar == null ? model.Image : model.Avatar.ToBase64String();
        return await _shopBackendService.PostAsync(guitarDto, Constants.Routes.AddGuitar);
    }
}