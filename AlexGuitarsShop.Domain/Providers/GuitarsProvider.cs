using AlexGuitarsShop.Common;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.Guitar;

namespace AlexGuitarsShop.Domain.Providers;

public class GuitarsProvider : IGuitarsProvider
{
    private readonly IGuitarRepository _guitarRepository;

    public GuitarsProvider(IGuitarRepository guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task<IResult<List<Common.Models.Guitar>>> GetGuitarsByLimitAsync(int offset, int limit)
    {
        var guitarsList = await _guitarRepository.GetAllAsync(offset, limit);
        return ResultCreator.GetValidResult(ListMapper.ToDtoGuitarList(guitarsList));
    }

    public async Task<IResult<Common.Models.Guitar>> GetGuitarAsync(int id)
    {
        Guitar guitar = await _guitarRepository.GetAsync(id);
        return ResultCreator.GetValidResult(guitar.ToGuitarDto());
    }

    public async Task<IResult<int>> GetCountAsync()
    {
        int count = await _guitarRepository.GetCountAsync();
        return ResultCreator.GetValidResult(count);
    }
}