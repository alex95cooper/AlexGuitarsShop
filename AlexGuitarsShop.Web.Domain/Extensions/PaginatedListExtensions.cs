using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Extensions;

public static class PaginatedListExtensions
{
    public static PaginatedListViewModel<T> ToPaginatedListViewModel<T>(this PaginatedList<T> list, Title title, int pageNumber)
    {
        return new PaginatedListViewModel<T>
        {
            Title = title,
            List = list.LimitedList,
            TotalCount = list.CountOfAll,
            CurrentPage = pageNumber
        };
    }
}