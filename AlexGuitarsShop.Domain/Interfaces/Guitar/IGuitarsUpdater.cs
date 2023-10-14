using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarsUpdater
{
    Task<IResult> UpdateGuitarAsync(GuitarDto guitarDto);

    Task<IResult> DeleteGuitarAsync(int id);
}