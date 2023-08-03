using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Extensions;

public static class GuitarExtensions
{
    public static GuitarViewModel ToGuitarViewModel(this Guitar guitar)
    {
        if (guitar == null) throw new ArgumentNullException(nameof(guitar));
        return new GuitarViewModel(guitar.Id, guitar.Name, guitar.Price, 
            guitar.Description, null, guitar.Image, guitar.IsDeleted);
    }
}