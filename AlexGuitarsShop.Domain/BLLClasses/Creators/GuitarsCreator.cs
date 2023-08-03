using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.BLLClasses.Creators;

public class GuitarsCreator : IGuitarsCreator
{
    private readonly IGuitarRepository _guitarRepository;

    public GuitarsCreator(IGuitarRepository guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task AddGuitarAsync(GuitarViewModel model)
    {
        if (_guitarRepository == null) throw new ArgumentNullException(nameof(_guitarRepository));
        Guitar guitar = model.ToGuitar();
        await _guitarRepository.AddAsync(guitar)!;
    }
}