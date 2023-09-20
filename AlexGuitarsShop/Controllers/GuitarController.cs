using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.Validators;
using Microsoft.AspNetCore.Mvc;
using GuitarDto = AlexGuitarsShop.Common.Models.Guitar;

namespace AlexGuitarsShop.Controllers;

[ApiController]
[Route("[controller]")]
public class GuitarController : Controller
{
    private readonly IGuitarsCreator _guitarsCreator;
    private readonly IGuitarsProvider _guitarsProvider;
    private readonly IGuitarsUpdater _guitarsUpdater;
    private readonly IGuitarValidator _guitarValidator;

    public GuitarController(IGuitarsCreator guitarsCreator,
        IGuitarsProvider guitarsProvider, IGuitarValidator guitarValidator, IGuitarsUpdater guitarsUpdater)
    {
        _guitarsCreator = guitarsCreator;
        _guitarsProvider = guitarsProvider;
        _guitarsUpdater = guitarsUpdater;
        _guitarValidator = guitarValidator;
    }

    [HttpGet("Index/{pageNumber}")]
    public async Task<IResult<PaginatedList<GuitarDto>>> Index(int pageNumber = 1)
    {
        int count = (await _guitarsProvider.GetCountAsync()).Data;
        if (!PageValidator.CheckIfPageIsValid(pageNumber, Paginator.Limit, count))
        {
            return ResultCreator.GetInvalidResult<PaginatedList<GuitarDto>>(
                Constants.ErrorMessages.InvalidPage);
        }

        int offset = Paginator.GetOffset(pageNumber);
        List<GuitarDto> list = (await _guitarsProvider.GetGuitarsByLimitAsync(offset, Paginator.Limit)).Data;
        return ResultCreator.GetValidResult(
            new PaginatedList<GuitarDto>
            {
                CountOfAll = count,
                LimitedList = list
            });
    }
    
    [HttpGet("Update/{id}")]
    public async Task<IResult<GuitarDto>> Update(int id)
    {
        if (!await _guitarValidator.CheckIfGuitarExist(id))
        {
            return ResultCreator.GetInvalidResult<GuitarDto>(
                Constants.ErrorMessages.InvalidGuitarId);
        }

        var result = await _guitarsProvider.GetGuitarAsync(id);
        return ResultCreator.GetValidResult(result.Data);
    }

    [HttpPost("Add")]
    public async Task<IResult<GuitarDto>> Add(GuitarDto guitarDto)
    {

        if (guitarDto == null || !_guitarValidator.CheckIfGuitarIsValid(guitarDto))
        {
            return ResultCreator.GetInvalidResult<GuitarDto>(
                Constants.ErrorMessages.InvalidGuitar);
        }
        
        await _guitarsCreator.AddGuitarAsync(guitarDto);
        return ResultCreator.GetValidResult(guitarDto);
    }

    [HttpPost("Update")]
    public async Task<IResult<GuitarDto>> Update(GuitarDto guitarDto)
    {

        if (guitarDto == null || !await _guitarValidator.CheckIfGuitarUpdateIsValid(guitarDto))
        {
            return ResultCreator.GetInvalidResult<GuitarDto>(
                Constants.ErrorMessages.InvalidGuitar);
        }
        
        await _guitarsUpdater.UpdateGuitarAsync(guitarDto);
        return ResultCreator.GetValidResult(guitarDto);
    }

    [HttpGet("Delete/{id}")]
    public async Task<IResult<int>> Delete(int id)
    {
        if (!await _guitarValidator.CheckIfGuitarExist(id))
        {
            return ResultCreator.GetInvalidResult<int>(
                Constants.ErrorMessages.InvalidGuitarId);
        }

        await _guitarsUpdater.DeleteGuitarAsync(id);
        return ResultCreator.GetValidResult(id);
    }
}