using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarsProvider
{
    Task<IResult<List<DAL.Models.Guitar>>> GetGuitarsByLimitAsync(int offset, int fetchRows);

    Task<IResult<GuitarViewModel>> GetGuitarAsync(int id);

    Task<IResult<int>> GetCountAsync();
}