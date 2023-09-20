using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Extensions;

public static class RegisterViewModelExtensions
{
    public static Register ToRegister(this RegisterViewModel model)
    {
        return new Register
        {
            Name = model.Name, 
            Email = model.Email, 
            Password = model.Password
        };
    }
}