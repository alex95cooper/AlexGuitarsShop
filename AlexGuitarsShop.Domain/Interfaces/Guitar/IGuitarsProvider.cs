using AlexGuitarsShop.Common;

namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarsProvider
{
    Task<IResult<List<Common.Models.Guitar>>> GetGuitarsByLimitAsync(int offset, int limit);

    Task<IResult<Common.Models.Guitar>> GetGuitarAsync(int id);

    Task<IResult<int>> GetCountAsync();
}