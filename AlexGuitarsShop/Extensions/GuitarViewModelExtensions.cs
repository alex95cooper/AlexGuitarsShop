using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.ViewModels;

namespace AlexGuitarsShop.Extensions;

public static class GuitarViewModelExtensions
{
    public static Guitar ToGuitar(this GuitarViewModel model)
    {
        return new Guitar(model.Id, model.Name,
            model.Price, model.Description, model.Image, model.IsDeleted);
    }
}