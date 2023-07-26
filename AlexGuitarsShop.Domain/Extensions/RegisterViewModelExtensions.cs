using AlexGuitarsShop.DAL;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Extensions;

public static class RegisterViewModelExtensions
{
    public static User ToUser(this RegisterViewModel model)
    {
        return new User
        {
            Name = model!.Name,
            Email = model.Email,
            Role = Role.User,
            Password = PasswordHasher.HashPassword(model.Password),
        };
    }
}