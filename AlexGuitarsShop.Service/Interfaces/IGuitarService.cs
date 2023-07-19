using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Models;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Service.Interfaces;

public interface IGuitarService
{
    Task<IResponse<List<Guitar>>> GetGuitarsByLimit(int offset, int fetchRows);
    Task<IResponse<GuitarViewModel>> GetGuitar(int id);
    Task AddGuitar (GuitarViewModel model);
    Task UpdateGuitar(GuitarViewModel model);
    Task DeleteGuitar(int id);
    Task<IResponse<int>> GetCount();
    Guitar ToGuitar(GuitarViewModel model);
}