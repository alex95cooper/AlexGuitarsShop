using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Extensions;

public static class GuitarViewModelExtensions
{
    public static Guitar ToGuitar(this GuitarViewModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        return new Guitar(model.Id, model.Name, 
            model.Price, model.Description, model.Image, model.IsDeleted);
    }
}