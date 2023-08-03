namespace AlexGuitarsShop.Domain.ViewModels;

public record ListViewModel<T>(List<T> List, Title Title, int PageCount, int CurrentPage) : 
    PaginationViewModel(Title, PageCount, CurrentPage);
