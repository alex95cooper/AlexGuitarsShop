using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarValidator
{
    Task<IResult> CheckIfGuitarExist(int id);

    IResult CheckIfGuitarIsValid(GuitarDto guitarDto);

    Task<IResult> CheckIfGuitarUpdateIsValid(GuitarDto guitarDto);
}