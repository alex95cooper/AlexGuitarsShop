using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Guitar;

public interface IGuitarsProvider
{
    Task<IResultDto<PaginatedListViewModel<GuitarDto>>> GetGuitarsByLimitAsync(int pageNumber);

    Task<IResultDto<GuitarViewModel>> GetGuitarAsync(int id);
}