using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.EntityHandlers.GuitarsHandlers;

public class GuitarsProvider : IGuitarsProvider
{
    private readonly IRepository<Guitar> _guitarRepository;

    public GuitarsProvider(IRepository<Guitar> guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task<IResponse<List<Guitar>>> GetGuitarsByLimitAsync(int offset, int limit)
    {
        return new Response<List<Guitar>>
        {
            Data = await _guitarRepository!.SelectByLimitAsync(offset, limit)!
        };
    }

    public async Task<IResponse<GuitarViewModel>> GetGuitarAsync(int id)
    {
        Guitar guitar = await _guitarRepository!.GetAsync(id)!;
        return new Response<GuitarViewModel>
        {
            Data = new GuitarViewModel
            {
                Id = guitar!.Id,
                Name = guitar.Name,
                Price = guitar.Price,
                Description = guitar.Description,
                Image = guitar.Image,
                IsDeleted = guitar.IsDeleted
            }
        };
    }

    public async Task<IResponse<int>> GetCountAsync()
    {
        return new Response<int>
        {
            Data = await _guitarRepository!.GetCountAsync()!
        };
    }
}