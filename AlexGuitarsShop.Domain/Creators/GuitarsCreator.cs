using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.Domain.Creators;

public class GuitarsCreator : IGuitarsCreator
{
    private readonly IGuitarRepository _guitarRepository;

    public GuitarsCreator(IGuitarRepository guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task<IResult<GuitarDto>> AddGuitarAsync(GuitarDto guitarDto)
    {
        Guitar guitarDal = guitarDto.ToGuitarDal();
        await _guitarRepository.AddAsync(guitarDal);
        return ResultCreator.GetValidResult(guitarDto, HttpStatusCode.OK);
    }
}