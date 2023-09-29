using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Updaters;

public class GuitarsUpdater : IGuitarsUpdater
{
    private const string ErrorMessage = "The information about the account is not filled correctly!";

    private readonly IResponseMaker _responseMaker;

    public GuitarsUpdater(IResponseMaker responseMaker)
    {
        _responseMaker = responseMaker;
    }

    public async Task<IResult<GuitarDto>> UpdateGuitarAsync(GuitarViewModel model)
    {
        if (model == null)
        {
            return ResultCreator.GetInvalidResult<GuitarDto>(
                ErrorMessage, HttpStatusCode.BadRequest);
        }

        GuitarDto guitarDto = model.ToGuitar();
        guitarDto.Image = model.Avatar == null ? model.Image : model.Avatar.ToBase64String();
        return await _responseMaker.PutAsync(guitarDto, Constants.Routes.UpdateGuitar);
    }

    public async Task<IResult<int>> DeleteGuitarAsync(int id)
    {
        return await _responseMaker.DeleteAsync(string.Format(Constants.Routes.DeleteGuitar, id));
    }
}