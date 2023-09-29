using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.Domain.Extensions;

public static class GuitarExtensions
{
    public static GuitarDto ToGuitarDto(this Guitar guitar)
    {
        return new GuitarDto
        {
            Id = guitar.Id,
            Name = guitar.Name,
            Price = guitar.Price,
            Description = guitar.Description,
            Image = guitar.Image,
            IsDeleted = guitar.IsDeleted
        };
    }
    
    public static Guitar ToGuitarDal(this GuitarDto guitarDto)
    {
        return new Guitar
        {
            Id = guitarDto.Id,
            Name = guitarDto.Name,
            Price = guitarDto.Price,
            Description = guitarDto.Description,
            Image = guitarDto.Image,
            IsDeleted = guitarDto.IsDeleted
        };
    }
}