using System.ComponentModel.DataAnnotations;
using AlexGuitarsShop.Common;

namespace AlexGuitarsShop.DAL.Models;

public class Account
{
    [Key] public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public virtual List<CartItem> CartItems { get; set; }
}