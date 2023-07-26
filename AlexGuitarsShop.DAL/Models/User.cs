namespace AlexGuitarsShop.DAL.Models;

public class User 
{
    public string Name{ get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public Role Role { get; init; }
}