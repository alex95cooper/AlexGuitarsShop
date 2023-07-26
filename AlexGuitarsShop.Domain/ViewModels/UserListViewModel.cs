using AlexGuitarsShop.DAL;
using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.Domain.ViewModels;

public class UserListViewModel
{
    public List<User> Users { get; init; }
    public Role Role { get; init; }
    public int PageCount { get; init; }
    public int CurrentPage { get; init; }
}