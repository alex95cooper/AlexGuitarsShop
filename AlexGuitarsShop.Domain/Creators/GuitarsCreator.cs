using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces.Guitar;

namespace AlexGuitarsShop.Domain.Creators;

public class GuitarsCreator : IGuitarsCreator
{
    private readonly IGuitarRepository _guitarRepository;

    public GuitarsCreator(IGuitarRepository guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task AddGuitarAsync(Guitar guitar)
    {
        guitar = guitar ?? throw new ArgumentNullException(nameof(guitar));
        await _guitarRepository.AddAsync(guitar);
    }
}