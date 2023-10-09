namespace AlexGuitarsShop.Web.Domain.ViewModels;

public class PaginatedListViewModel<T> : PaginationViewModel
{
    public List<T> List { get; init; }
}