using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.EntityHandlers.GuitarsHandlers;

public class GuitarsUpdater : IGuitarsUpdater
{
    private readonly IRepository<Guitar> _guitarRepository;

    public GuitarsUpdater(IRepository<Guitar> guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task UpdateGuitarAsync(GuitarViewModel model)
    {
        Guitar guitar = model.ToGuitar();
        await _guitarRepository!.UpdateAsync(guitar)!;
    }

    public async Task DeleteGuitarAsync(int id)
    {
        await _guitarRepository!.DeleteAsync(id)!;
    }
}