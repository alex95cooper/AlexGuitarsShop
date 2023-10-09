using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Extensions;

public static class PaginatedListExtensions
{
    public static PaginatedListViewModel<T> ToPaginatedListViewModel<T>(
        this PaginatedListDto<T> listDto, Title title, int pageNumber)
    {
        return new PaginatedListViewModel<T>
        {
            Title = title,
            List = listDto.LimitedList,
            TotalCount = listDto.CountOfAll,
            CurrentPage = pageNumber
        };
    }
}