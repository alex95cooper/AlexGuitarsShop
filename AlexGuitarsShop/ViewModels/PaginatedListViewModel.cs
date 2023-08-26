namespace AlexGuitarsShop.ViewModels;

public class PaginatedListViewModel<T> : PaginationViewModel
{
    public List<T> List { get; init; }
}