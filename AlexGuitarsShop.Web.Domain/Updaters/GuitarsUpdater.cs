using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Updaters;

public class GuitarsUpdater : IGuitarsUpdater
{
    private const string ErrorMessage = "The information about the account is not filled correctly!";

    private readonly IShopBackendService _shopBackendService;

    public GuitarsUpdater(IShopBackendService shopBackendService)
    {
        _shopBackendService = shopBackendService;
    }

    public async Task<IResultDto> UpdateGuitarAsync(GuitarViewModel model)
    {
        if (model == null)
        {
            return ResultDtoCreator.GetInvalidResult<GuitarDto>(ErrorMessage);
        }

        GuitarDto guitarDto = model.ToGuitarDto();
        guitarDto.Image = model.Avatar == null ? model.Image : model.Avatar.ToBase64String();
        return await _shopBackendService.PutAsync(guitarDto,
            string.Format(Constants.Routes.UpdateGuitar, guitarDto.Id));
    }

    public async Task<IResultDto> DeleteGuitarAsync(int id)
    {
        return await _shopBackendService.DeleteAsync(string.Format(Constants.Routes.DeleteGuitar, id));
    }
}