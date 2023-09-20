using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using GuitarDal = AlexGuitarsShop.DAL.Models.Guitar;

namespace AlexGuitarsShop.Domain.Creators;

public class GuitarsCreator : IGuitarsCreator
{
    private readonly IGuitarRepository _guitarRepository;

    public GuitarsCreator(IGuitarRepository guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task AddGuitarAsync(Common.Models.Guitar guitar)
    {
        GuitarDal guitarDal = guitar.ToGuitarDal() ?? throw new ArgumentNullException(nameof(guitar));
        await _guitarRepository.AddAsync(guitarDal);
    }
}