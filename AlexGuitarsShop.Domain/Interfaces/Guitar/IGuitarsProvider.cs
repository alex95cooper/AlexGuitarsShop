using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarsProvider
{
    Task<IResult<List<GuitarDto>>> GetGuitarsByLimitAsync(int offset, int limit);

    Task<IResult<GuitarDto>> GetGuitarAsync(int id);

    Task<IResult<DAL.Models.Guitar>> GetReferenceGuitarAsync(int id);

    Task<IResult<int>> GetCountAsync();
}