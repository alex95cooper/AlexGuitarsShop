using AlexGuitarsShop.DAL;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Extensions;

public static class RegisterViewModelExtensions
{
    public static Account ToUser(this RegisterViewModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        return new Account(model.Name, model.Email, 
            PasswordHasher.HashPassword(model.Password), Role.User);
    }
}