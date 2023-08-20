using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Guitar;

namespace AlexGuitarsShop.Domain.Providers;

public class GuitarsProvider : IGuitarsProvider
{
    private readonly IGuitarRepository _guitarRepository;

    public GuitarsProvider(IGuitarRepository guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task<IResult<List<Guitar>>> GetGuitarsByLimitAsync(int offset, int limit)
    {
        var guitarsList = await _guitarRepository.GetAllAsync(offset, limit);
        return ResultCreator.GetValidResult(guitarsList);
    }

    public async Task<IResult<Guitar>> GetGuitarAsync(int id)
    {
        Guitar guitar = await _guitarRepository.GetAsync(id);
        return ResultCreator.GetValidResult(guitar);
    }

    public async Task<IResult<int>> GetCountAsync()
    {
        int count = await _guitarRepository.GetCountAsync();
        return ResultCreator.GetValidResult(count);
    }
}