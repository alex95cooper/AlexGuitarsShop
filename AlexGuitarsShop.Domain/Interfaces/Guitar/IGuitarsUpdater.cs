using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarsUpdater
{
    Task<IResult<GuitarDto>> UpdateGuitarAsync(GuitarDto guitarDto);

    Task<IResult<int>> DeleteGuitarAsync(int id);
}