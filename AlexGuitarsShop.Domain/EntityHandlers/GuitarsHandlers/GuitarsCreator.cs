using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.EntityHandlers.GuitarsHandlers;

public class GuitarsCreator : IGuitarsCreator
{
    private readonly IRepository<Guitar> _guitarRepository;

    public GuitarsCreator(IRepository<Guitar> guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task AddGuitarAsync(GuitarViewModel model)
    {
        Guitar guitar = model.ToGuitar();
        await _guitarRepository!.AddAsync(guitar)!;
    }
}