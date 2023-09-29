using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarValidator
{
    Task<IResult<GuitarDto>> CheckIfGuitarExist(int id);
    
    IResult<GuitarDto> CheckIfGuitarIsValid(GuitarDto guitarDto);

    Task<IResult<GuitarDto>> CheckIfGuitarUpdateIsValid(GuitarDto guitarDto);
}