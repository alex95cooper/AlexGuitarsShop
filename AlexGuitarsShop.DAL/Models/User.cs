namespace AlexGuitarsShop.DAL.Models;

public record User(string Name,
    string Email, string Password, Role Role)
{
    public User() : this(default, default, default,
        default) { }
}