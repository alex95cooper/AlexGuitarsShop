using AlexGuitarsShop.Domain;
using AlexGuitarsShop.ViewModels;

namespace AlexGuitarsShop.Extensions;

public static class ListExtensions
{
    public static PaginatedListViewModel<T> ToPaginatedList<T>(this List<T> list, Title title, int count, int pageNumber)
    {
        return new PaginatedListViewModel<T>
        {
            List = list, Title = title,
            TotalCount = count, CurrentPage = pageNumber
        };
    }
}