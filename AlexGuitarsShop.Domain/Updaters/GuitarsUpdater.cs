using System.Net;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.Domain.Updaters;

public class GuitarsUpdater : IGuitarsUpdater
{
    private readonly IGuitarRepository _guitarRepository;

    public GuitarsUpdater(IGuitarRepository guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task<IResult<GuitarDto>> UpdateGuitarAsync(GuitarDto guitarDto)
    {
        Guitar guitarDal = _guitarRepository.GetAsync(guitarDto.Id).Result;
        if (guitarDal != null)
        {
            guitarDal.Name = guitarDto.Name;
            guitarDal.Price = guitarDto.Price;
            guitarDal.Image = guitarDto.Image;
            guitarDal.Description = guitarDto.Description;
        }

        await _guitarRepository.UpdateAsync(guitarDal);
        return ResultCreator.GetValidResult(guitarDto, HttpStatusCode.OK);
    }

    public async Task<IResult<int>> DeleteGuitarAsync(int id)
    {
        await _guitarRepository.DeleteAsync(id);
        return ResultCreator.GetValidResult(id, HttpStatusCode.OK);
    }
}