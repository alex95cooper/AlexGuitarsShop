using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using GuitarDal = AlexGuitarsShop.DAL.Models.Guitar;

namespace AlexGuitarsShop.Domain.Updaters;

public class GuitarsUpdater : IGuitarsUpdater
{
    private readonly IGuitarRepository _guitarRepository;

    public GuitarsUpdater(IGuitarRepository guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task UpdateGuitarAsync(Common.Models.Guitar guitar)
    {
        GuitarDal guitarDal = guitar.ToGuitarDal() ?? throw new ArgumentNullException(nameof(guitar));
        await _guitarRepository.UpdateAsync(guitarDal);
    }

    public async Task DeleteGuitarAsync(int id)
    {
        await _guitarRepository.DeleteAsync(id);
    }
}