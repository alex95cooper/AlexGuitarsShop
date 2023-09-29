using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.Domain.Extensions;

public static class AccountDalExtensions
{
    public static AccountDto ToAccountDto(this Account account)
    {
        return new AccountDto
        {
            Name = account.Name,
            Email = account.Email,
            Role = account.Role
        };
    }
}