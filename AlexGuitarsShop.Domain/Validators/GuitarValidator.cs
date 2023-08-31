using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces.Guitar;

namespace AlexGuitarsShop.Domain.Validators;

public class GuitarValidator : IGuitarValidator
{
    private readonly IGuitarRepository _guitarRepository;

    public GuitarValidator(IGuitarRepository guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task<bool> CheckIfGuitarExist(int id)
    {
        return await _guitarRepository.GetAsync(id) != null;
    }

    public bool CheckIfGuitarIsValid(Guitar guitar)
    {
        return guitar != null
               && CheckIfNameIsValid(guitar.Name)
               && CheckIfPriceIsValid(guitar.Price)
               && CheckIfDescriptionIsValid(guitar.Description);
    }

    public async Task<bool> CheckIfGuitarUpdateIsValid(Guitar guitar)
    {
        return await CheckIfGuitarExist(guitar.Id)
               && CheckIfGuitarIsValid(guitar);
    }

    private bool CheckIfNameIsValid(string name)
    {
        if (name == null)
        {
            return false;
        }

        return name.Length is >= Constants.Guitar.NameMinLength
            and <= Constants.Guitar.NameMaxLength;
    }

    private static bool CheckIfPriceIsValid(int price)
    {
        return price is >= Constants.Guitar.MinPrice
            and <= Constants.Guitar.MaxPrice;
    }

    private static bool CheckIfDescriptionIsValid(string description)
    {
        if (description == null)
        {
            return false;
        }

        return description.Length is >= Constants.Guitar.DescriptionMinLength
            and <= Constants.Guitar.DescriptionMaxLength;
    }
}