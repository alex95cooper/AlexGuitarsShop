using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarsCreator
{
    Task<IResult<GuitarDto>> AddGuitarAsync(GuitarDto guitarDto);
}