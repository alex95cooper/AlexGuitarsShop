using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.BLLClasses.Providers;

public class GuitarsProvider : IGuitarsProvider
{
    private readonly IGuitarRepository _guitarRepository;

    public GuitarsProvider(IGuitarRepository guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task<IResult<List<Guitar>>> GetGuitarsByLimitAsync(int offset, int limit)
    {
        if (_guitarRepository == null) throw new ArgumentNullException(nameof(_guitarRepository));
        var guitarsList = await _guitarRepository.GetAllAsync(offset, limit)!;
        return ResultCreator.GetValidResult(guitarsList);
    }

    public async Task<IResult<GuitarViewModel>> GetGuitarAsync(int id)
    {
        if (_guitarRepository == null) throw new ArgumentNullException(nameof(_guitarRepository));
        Guitar guitar = await _guitarRepository.GetAsync(id)!;
        return ResultCreator.GetValidResult(guitar.ToGuitarViewModel());
    }

    public async Task<IResult<int>> GetCountAsync()
    {
        if (_guitarRepository == null) throw new ArgumentNullException(nameof(_guitarRepository));
        int count = await _guitarRepository.GetCountAsync()!;
        return ResultCreator.GetValidResult(count);
    }
}