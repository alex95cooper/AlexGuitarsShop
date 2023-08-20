using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Domain.Models;
using AlexGuitarsShop.ViewModels;

namespace AlexGuitarsShop.Extensions;

public static class RegisterViewModelExtensions
{
    public static Register ToRegister(this RegisterViewModel model)
    {
        return new Register(model.Name, model.Email,
            PasswordHasher.HashPassword(model.Password));
    }
}