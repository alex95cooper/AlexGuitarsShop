using AlexGuitarsShop.Domain;

namespace AlexGuitarsShop.ViewModels;

public class PaginationViewModel
{
    public Title Title { get; init; }
    public int TotalCount { get; init; }
    public int CurrentPage { get; init; }
}