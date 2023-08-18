using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Extensions;

public static class GuitarExtensions
{
    public static GuitarViewModel ToGuitarViewModel(this Guitar guitar)
    {
        if (guitar == null) throw new ArgumentNullException(nameof(guitar));
        return new GuitarViewModel
        {
            Id = guitar.Id, Name = guitar.Name, Price = guitar.Price,
            Description = guitar.Description, Image = guitar.Image, IsDeleted = guitar.IsDeleted
        };
    }
}