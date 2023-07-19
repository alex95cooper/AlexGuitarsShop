using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Models;
using AlexGuitarsShop.Domain.Responses;
using AlexGuitarsShop.Domain.ViewModels;
using AlexGuitarsShop.Service.Interfaces;

namespace AlexGuitarsShop.Service.Services;

public class GuitarService : IGuitarService
{
    private readonly IRepository<Guitar> _guitarRepository;

    public GuitarService(IRepository<Guitar> guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task<IResponse<List<Guitar>>> GetGuitarsByLimit(int offset, int limit)
    {
        return new Response<List<Guitar>>
        {
            Data = await _guitarRepository.SelectByLimit(offset, limit)
        };
    }
    
    public async Task< IResponse<GuitarViewModel>> GetGuitar(int id)
    {
        Guitar guitar = await _guitarRepository.Get(id);
        return new Response<GuitarViewModel>
        {
            Data = new GuitarViewModel
            {
                Id = guitar.Id,
                Name = guitar.Name, 
                Price = guitar.Price,
                Description = guitar.Description,
                Image = guitar.Image,
                IsDeleted = guitar.IsDeleted
            }
        };
    }

    public async Task AddGuitar(GuitarViewModel model)
    {
        Guitar guitar = ToGuitar(model);
        await _guitarRepository.Add(guitar);
    }

    public async Task UpdateGuitar(GuitarViewModel model)
    {
        Guitar guitar = ToGuitar(model);
        _guitarRepository.Update(guitar);
    }

    public async Task DeleteGuitar(int id)
    {
        await _guitarRepository.Delete(id);
    }

    public async Task<IResponse<int>> GetCount()
    {
        return new Response<int>
        {
            Data = await _guitarRepository.GetCount()
        };
    }

    public  Guitar ToGuitar(GuitarViewModel model)
    {
        return new Guitar
        {
            Id = model.Id,
            Name = model.Name, 
            Price = model.Price,
            Description = model.Description,
            Image = model.Image,
            IsDeleted = model.IsDeleted
        };
    }
}