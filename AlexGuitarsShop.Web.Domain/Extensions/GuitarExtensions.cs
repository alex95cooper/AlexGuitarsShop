using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Extensions;

public static class GuitarExtensions
{
    public static GuitarDto ToGuitarDto(this GuitarViewModel model)
    {
        return new GuitarDto
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            Description = model.Description,
            Image = model.Image,
            IsDeleted = model.IsDeleted
        };
    }

    public static GuitarViewModel ToGuitarViewModel(this GuitarDto guitarDto)
    {
        return new GuitarViewModel
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