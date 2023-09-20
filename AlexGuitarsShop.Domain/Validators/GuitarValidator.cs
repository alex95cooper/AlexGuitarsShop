using AlexGuitarsShop.DAL.Interfaces;
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

    public bool CheckIfGuitarIsValid(Common.Models.Guitar guitar)
    {
        return guitar != null
               && CheckIfNameIsValid(guitar.Name)
               && CheckIfPriceIsValid(guitar.Price)
               && CheckIfDescriptionIsValid(guitar.Description);
    }

    public async Task<bool> CheckIfGuitarUpdateIsValid(Common.Models.Guitar guitar)
    {
        return await CheckIfGuitarExist(guitar.Id)
               && CheckIfGuitarIsValid(guitar);
    }

    private static bool CheckIfNameIsValid(string name)
    {
        if (name == null)
        {
            return false;
        }

        bool nameIsGood = name.Length is >= Constants.Guitar.NameMinLength
            and <= Constants.Guitar.NameMaxLength;
        return nameIsGood;
    }

    private static bool CheckIfPriceIsValid(int price)
    {
        bool priceIsGood = price is >= Constants.Guitar.MinPrice
            and <= Constants.Guitar.MaxPrice;
        return priceIsGood;
    }

    private static bool CheckIfDescriptionIsValid(string description)
    {
        if (description == null)
        {
            return false;
        }

        bool descriptionIsGood = description.Length is >= Constants.Guitar.DescriptionMinLength
            and <= Constants.Guitar.DescriptionMaxLength;
        return descriptionIsGood;
    }
}