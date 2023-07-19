using AlexGuitarsShop.Domain.Models;

namespace AlexGuitarsShop.Domain.ViewModels;

public class UserListViewModel
{
    public List<User> Users { get; set; }
    public string Role { get; set; }
    public int PageCount { get; set; }
    public int CurrentPage { get; set; }
}