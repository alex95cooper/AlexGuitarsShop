using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Guitar;

public interface IGuitarsProvider
{
    Task<IResult<PaginatedListViewModel<GuitarDto>>> GetGuitarsByLimitAsync(int pageNumber);

    Task<IResult<GuitarViewModel>> GetGuitarAsync(int id);
}