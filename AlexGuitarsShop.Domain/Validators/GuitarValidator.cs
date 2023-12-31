using System.Net;
using AlexGuitarsShop.Common.Models;
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

    public async Task<IResult> CheckIfGuitarExist(int id)
    {
        return await _guitarRepository.GetAsync(id) != null
            ? ResultCreator.GetValidResult()
            : ResultCreator.GetInvalidResult(
                Constants.ErrorMessages.InvalidGuitarId, HttpStatusCode.BadRequest);
    }

    public IResult CheckIfGuitarIsValid(GuitarDto guitarDto)
    {
        if (guitarDto != null
            && CheckIfNameIsValid(guitarDto.Name)
            && CheckIfPriceIsValid(guitarDto.Price)
            && CheckIfDescriptionIsValid(guitarDto.Description))
        {
            return ResultCreator.GetValidResult();
        }

        return ResultCreator.GetInvalidResult(
            Constants.ErrorMessages.InvalidGuitar, HttpStatusCode.BadRequest);
    }

    public async Task<IResult> CheckIfGuitarUpdateIsValid(GuitarDto guitarDto)
    {
        var result = await CheckIfGuitarExist(guitarDto.Id);
        if (result.IsSuccess && CheckIfGuitarIsValid(guitarDto).IsSuccess)
        {
            return ResultCreator.GetValidResult();
        }

        return ResultCreator.GetInvalidResult(
            Constants.ErrorMessages.InvalidGuitar, HttpStatusCode.BadRequest);
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