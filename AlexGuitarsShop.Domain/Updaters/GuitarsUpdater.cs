using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces.Guitar;

namespace AlexGuitarsShop.Domain.Updaters;

public class GuitarsUpdater : IGuitarsUpdater
{
    private readonly IGuitarRepository _guitarRepository;

    public GuitarsUpdater(IGuitarRepository guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task UpdateGuitarAsync(Guitar guitar)
    {
        guitar = guitar ?? throw new ArgumentNullException(nameof(guitar));
        await _guitarRepository.UpdateAsync(guitar);
    }

    public async Task DeleteGuitarAsync(int id)
    {
        await _guitarRepository.DeleteAsync(id);
    }
}