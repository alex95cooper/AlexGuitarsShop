namespace AlexGuitarsShop.DAL.Models;

public class Account
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; set; }
    public Role Role { get; init; }
}