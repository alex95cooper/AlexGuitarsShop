using AlexGuitarsShop.DAL;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Models;

namespace AlexGuitarsShop.Domain.Extensions;

public static class RegisterExtensions
{
    public static Account ToAccount(this Register register)
    {
        return new Account
        {
            Name = register.Name,
            Email = register.Email,
            Password = register.Password,
            CartId = Guid.NewGuid().ToString(),
            Role = Role.User
        };
    }
}