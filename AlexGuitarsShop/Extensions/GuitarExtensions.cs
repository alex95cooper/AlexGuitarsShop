using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.ViewModels;

namespace AlexGuitarsShop.Extensions;

public static class GuitarExtensions
{
    public static GuitarViewModel ToGuitarViewModel(this Guitar guitar)
    {
        return new GuitarViewModel
        {
            Id = guitar.Id, Name = guitar.Name, Price = guitar.Price,
            Description = guitar.Description, Image = guitar.Image, IsDeleted = guitar.IsDeleted
        };
    }
}