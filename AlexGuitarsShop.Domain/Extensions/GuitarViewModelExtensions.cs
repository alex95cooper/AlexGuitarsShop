using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Extensions;

public static class GuitarViewModelExtensions
{
    public static Guitar ToGuitar(this GuitarViewModel model)
    {
        return new Guitar
        {
            Id = model!.Id,
            Name = model.Name,
            Price = model.Price,
            Description = model.Description,
            Image = model.Image,
            IsDeleted = model.IsDeleted
        };
    }
}