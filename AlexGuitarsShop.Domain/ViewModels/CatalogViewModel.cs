using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.Domain.ViewModels;

public class CatalogViewModel
{
    public List<Guitar> Guitars { get; init; }
    public int PageCount { get; init; }
    public int CurrentPage { get; init; }
}