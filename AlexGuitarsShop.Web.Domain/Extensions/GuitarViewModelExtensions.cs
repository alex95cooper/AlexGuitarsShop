using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Extensions;

public static class GuitarViewModelExtensions
{
    public static Guitar ToGuitar(this GuitarViewModel model)
    {
        return new Guitar
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price, 
            Description = model.Description, 
            Image = model.Image, 
            IsDeleted = model.IsDeleted
        };
    }
}