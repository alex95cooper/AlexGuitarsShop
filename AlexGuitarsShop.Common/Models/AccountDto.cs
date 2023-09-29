namespace AlexGuitarsShop.Common.Models;

public class AccountDto
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public Role Role { get; init; }
}