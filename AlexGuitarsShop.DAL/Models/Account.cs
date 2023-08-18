namespace AlexGuitarsShop.DAL.Models;

public record Account(string Name,
    string Email, string Password, Role Role)
{
    public Account() : this(default, default, default,
        default) { }
}