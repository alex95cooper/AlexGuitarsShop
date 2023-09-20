using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.Domain.Extensions;

public static class GuitarExtensions
{
    public static Common.Models.Guitar ToGuitarDto(this Guitar guitar)
    {
        return new Common.Models.Guitar
        {
            Id = guitar.Id,
            Name = guitar.Name,
            Price = guitar.Price,
            Description = guitar.Description,
            Image = guitar.Image,
            IsDeleted = guitar.IsDeleted
        };
    }
    
    public static Guitar ToGuitarDal(this Common.Models.Guitar guitar)
    {
        return new Guitar
        {
            Id = guitar.Id,
            Name = guitar.Name,
            Price = guitar.Price,
            Description = guitar.Description,
            Image = guitar.Image,
            IsDeleted = guitar.IsDeleted
        };
    }
}