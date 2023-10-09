using System.Net;
using AlexGuitarsShop.Common.Models;
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

    public async Task<IResult<List<GuitarDto>>> GetGuitarsByLimitAsync(int offset, int limit)
    {
        var guitarsList = await _guitarRepository.GetAllAsync(offset, limit);
        var listDto = ListMapper.ToDtoGuitarList(guitarsList);
        return listDto.Count == 0
            ? ResultCreator.GetValidResult(listDto, HttpStatusCode.NoContent)
            : ResultCreator.GetValidResult(listDto, HttpStatusCode.OK);
    }

    public async Task<IResult<GuitarDto>> GetGuitarAsync(int id)
    {
        Guitar guitar = await _guitarRepository.GetAsync(id);
        return ResultCreator.GetValidResult(guitar.ToGuitarDto(), HttpStatusCode.OK);
    }

    public async Task<IResult<Guitar>> GetReferenceGuitarAsync(int id)
    {
        Guitar guitar = await _guitarRepository.GetAsync(id);
        return ResultCreator.GetValidResult(guitar, HttpStatusCode.OK);
    }

    public async Task<IResult<int>> GetCountAsync()
    {
        int count = await _guitarRepository.GetCountAsync();
        return ResultCreator.GetValidResult(count, HttpStatusCode.OK);
    }
}