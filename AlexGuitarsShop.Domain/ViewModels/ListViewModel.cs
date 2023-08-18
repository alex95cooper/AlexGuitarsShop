namespace AlexGuitarsShop.Domain.ViewModels;

public class ListViewModel<T> : PaginationViewModel
{
    public List<T> List { get; set; }
}