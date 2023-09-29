using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Extensions;

public static class LoginViewModelExtensions
{
    public static AccountDto ToAccountDto(this LoginViewModel model)
    {
        return new AccountDto
        {
            Email = model.Email,
            Password = model.Password
        };
    }
}