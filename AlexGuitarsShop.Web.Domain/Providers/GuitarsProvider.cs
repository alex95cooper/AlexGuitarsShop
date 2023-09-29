using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Providers;

public class GuitarsProvider : IGuitarsProvider
{
    private readonly IResponseMaker _responseMaker;

    public GuitarsProvider(IResponseMaker responseMaker)
    {
        _responseMaker = responseMaker;
    }

    public async Task<IResult<PaginatedListViewModel<GuitarDto>>> GetGuitarsByLimitAsync(int pageNumber)
    {
        var result = await _responseMaker.GetListByLimitAsync<GuitarDto>(Constants.Routes.GetGuitars, pageNumber);
        return result is {IsSuccess: true}
            ? ResultCreator.GetValidResult(result.Data.ToPaginatedListViewModel(Title.Catalog, pageNumber),
                result.StatusCode)
            : ResultCreator.GetInvalidResult<PaginatedListViewModel<GuitarDto>>(result!.Error, result.StatusCode);
    }

    public async Task<IResult<GuitarViewModel>> GetGuitarAsync(int id)
    {
        return await _responseMaker.GetGuitarAsync(id);
    }
}