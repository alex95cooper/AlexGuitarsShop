using AlexGuitarsShop.Common;
using AlexGuitarsShop.Web.Domain.ViewModels;
using GuitarDto = AlexGuitarsShop.Common.Models.Guitar;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Guitar;

public interface IGuitarsProvider
{
    Task<IResult<PaginatedListViewModel<GuitarDto>>> GetGuitarsByLimitAsync(int pageNumber);

    Task<IResult<GuitarViewModel>> GetGuitarAsync(int id);
}