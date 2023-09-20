using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using Account = AlexGuitarsShop.DAL.Models.Account;

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
            Role = Role.User
        };
    }
}