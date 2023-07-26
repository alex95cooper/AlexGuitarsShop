using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarsProvider
{
    Task<IResponse<List<DAL.Models.Guitar>>> GetGuitarsByLimitAsync(int offset, int fetchRows);

    Task<IResponse<GuitarViewModel>> GetGuitarAsync(int id);

    Task<IResponse<int>> GetCountAsync();
}