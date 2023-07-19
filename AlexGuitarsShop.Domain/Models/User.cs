using AlexGuitarsShop.Domain.Enums;

namespace AlexGuitarsShop.Domain.Models;

public class User 
{
    private int Id { get; set; }
    public string Name{ get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}