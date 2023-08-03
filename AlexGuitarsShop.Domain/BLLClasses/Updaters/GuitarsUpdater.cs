using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.BLLClasses.Updaters;

public class GuitarsUpdater : IGuitarsUpdater
{
    private readonly IGuitarRepository _guitarRepository;

    public GuitarsUpdater(IGuitarRepository guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task UpdateGuitarAsync(GuitarViewModel model)
    {
        if (_guitarRepository == null) throw new ArgumentNullException(nameof(_guitarRepository));
        Guitar guitar = model.ToGuitar();
        await _guitarRepository.UpdateAsync(guitar)!;
    }

    public async Task DeleteGuitarAsync(int id)
    {
        if (_guitarRepository == null) throw new ArgumentNullException(nameof(_guitarRepository));
        await _guitarRepository.DeleteAsync(id)!;
    }
}