namespace AlexGuitarsShop.ViewModels;

public class ListViewModel<T> : PaginationViewModel
{
    public List<T> List { get; init; }
}