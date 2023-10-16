using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Extensions;

public static class AccountExtensions
{
    public static AccountDto ToAccountDto(this LoginViewModel model)
    {
        return new AccountDto
        {
            Email = model.Email,
            Password = model.Password
        };
    }

    public static AccountDto ToAccountDto(this RegisterViewModel model)
    {
        return new AccountDto
        {
            Name = model.Name,
            Email = model.Email,
            Password = model.Password
        };
    }
}