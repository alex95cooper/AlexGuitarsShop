using AlexGuitarsShop.Domain.Models;

namespace AlexGuitarsShop.Domain.ViewModels;

public class CatalogViewModel
{
    public List<Guitar> Guitars { get; set; }
    public int PageCount { get; set; }
    public int CurrentPage { get; set; }
}