using AlexGuitarsShop.DAL;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Extensions;

public static class RegisterViewModelExtensions
{
    public static User ToUser(this RegisterViewModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        return new User(model.Name, model.Email, 
            PasswordHasher.HashPassword(model.Password), Role.User);
    }
}