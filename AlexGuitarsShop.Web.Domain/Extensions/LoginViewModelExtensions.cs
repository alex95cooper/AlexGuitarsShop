using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Extensions;

public static class LoginViewModelExtensions
{
    public static Login ToLogin(this LoginViewModel model)
    {
        return new Login
        {
            Email = model.Email, 
            Password = model.Password
        };
    }
}