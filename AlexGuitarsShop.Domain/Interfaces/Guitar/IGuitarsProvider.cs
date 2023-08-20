namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarsProvider
{
    Task<IResult<List<DAL.Models.Guitar>>> GetGuitarsByLimitAsync(int offset, int limit);

    Task<IResult<DAL.Models.Guitar>> GetGuitarAsync(int id);

    Task<IResult<int>> GetCountAsync();
}