using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Extensions;

public static class AccountDalExtensions
{
    public static Account ToAccount(this DAL.Models.Account account)
    {
        return new Account
        {
            Name = account.Name,
            Email = account.Email,
            Role = account.Role
        };
    }
}