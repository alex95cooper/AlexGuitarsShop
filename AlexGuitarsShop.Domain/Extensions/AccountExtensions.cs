using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.Domain.Extensions;

public static class AccountExtensions
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

    public static Account ToAccount(this AccountDto registerDto)
    {
        return new Account
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            Password = registerDto.Password,
            Role = Role.User
        };
    }
}