using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.Domain.Extensions;

public static class AccountDtoExtensions
{
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