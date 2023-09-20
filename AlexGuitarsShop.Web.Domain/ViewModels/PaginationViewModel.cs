namespace AlexGuitarsShop.Web.Domain.ViewModels;

public class PaginationViewModel
{
    public Title Title { get; init; }
    public int TotalCount { get; init; }
    public int CurrentPage { get; init; }
}