using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Creators;

public class GuitarsCreator : IGuitarsCreator
{
    private readonly IResponseMaker _responseMaker;

    public GuitarsCreator(IResponseMaker responseMaker)
    {
        _responseMaker = responseMaker;
    }

    public async Task<IResult<GuitarDto>> AddGuitarAsync(GuitarViewModel model)
    {
        if (model == null)
        {
            return ResultCreator.GetInvalidResult<GuitarDto>(
                Constants.Guitar.IncorrectGuitar, HttpStatusCode.BadRequest);
        }
        
        GuitarDto guitarDto = model.ToGuitar();
        guitarDto.Image = model!.Avatar == null ? model.Image : model.Avatar.ToBase64String();
        return await _responseMaker.PostAsync(guitarDto, Constants.Routes.AddGuitar);
    }
}