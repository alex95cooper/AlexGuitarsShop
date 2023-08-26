using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Domain.Models;
using AlexGuitarsShop.ViewModels;

namespace AlexGuitarsShop.Extensions;

public static class LoginViewModelExtensions
{
    public static Login ToLogin(this LoginViewModel model)
    {
        return new Login(model.Email, model.Password);
    }
}