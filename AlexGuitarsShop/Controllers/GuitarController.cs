using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.Validators;
using AlexGuitarsShop.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

[ApiController]
public class GuitarController : Controller
{
    private readonly IGuitarsCreator _guitarsCreator;
    private readonly IGuitarsProvider _guitarsProvider;
    private readonly IGuitarsUpdater _guitarsUpdater;
    private readonly IGuitarValidator _guitarValidator;
    private readonly ActionResultMaker _resultMaker;

    public GuitarController(IGuitarsCreator guitarsCreator,
        IGuitarsProvider guitarsProvider, IGuitarValidator guitarValidator, IGuitarsUpdater guitarsUpdater)
    {
        _guitarsCreator = guitarsCreator;
        _guitarsProvider = guitarsProvider;
        _guitarsUpdater = guitarsUpdater;
        _guitarValidator = guitarValidator;
        _resultMaker = new ActionResultMaker();
    }

    [HttpGet("guitars")]
    public async Task<ActionResult<ResultDto<PaginatedListDto<GuitarDto>>>> GetAll([FromQuery] int pageNumber = 1)
    {
        int count = (await _guitarsProvider.GetCountAsync()).Data;
        if (count == 0)
        {
            return _resultMaker.ResolveResult(ResultCreator.GetValidResult(
                new PaginatedListDto<AccountDto> {CountOfAll = 0, LimitedList = new List<AccountDto>()},
                HttpStatusCode.NoContent));
        }

        if (!PageValidator.CheckIfPageIsValid(pageNumber, Paginator.Limit, count))
        {
            return _resultMaker.ResolveResult(ResultCreator.GetInvalidResult<PaginatedListDto<GuitarDto>>(
                Constants.ErrorMessages.InvalidPage, HttpStatusCode.BadRequest));
        }

        int offset = Paginator.GetOffset(pageNumber);
        List<GuitarDto> list = (await _guitarsProvider.GetGuitarsByLimitAsync(offset, Paginator.Limit)).Data;
        return _resultMaker.ResolveResult(ResultCreator.GetValidResult(
            new PaginatedListDto<GuitarDto> {CountOfAll = count, LimitedList = list}, HttpStatusCode.OK));
    }

    [HttpGet("guitars/{id}")]
    public async Task<ActionResult<ResultDto<GuitarDto>>> Get([FromRoute] int id)
    {
        var validationResult = await _guitarValidator.CheckIfGuitarExist(id);
        if (validationResult.IsSuccess)
        {
            var result = await _guitarsProvider.GetGuitarAsync(id);
            return _resultMaker.ResolveResult(result);
        }

        return _resultMaker.ResolveResult(validationResult);
    }

    [HttpPost("guitars/add")]
    public async Task<ActionResult<ResultDto<GuitarDto>>> Add([FromBody] GuitarDto guitarDtoDto)
    {
        var validationResult = _guitarValidator.CheckIfGuitarIsValid(guitarDtoDto);
        if (validationResult.IsSuccess)
        {
            var result = await _guitarsCreator.AddGuitarAsync(guitarDtoDto);
            return _resultMaker.ResolveResult(result);
        }

        return _resultMaker.ResolveResult(validationResult);
    }

    [HttpPut("guitars/{id}/update")]
    public async Task<ActionResult<ResultDto<GuitarDto>>> Update([FromBody] GuitarDto guitarDtoDto)
    {
        var validationResult = await _guitarValidator.CheckIfGuitarUpdateIsValid(guitarDtoDto);
        if (validationResult.IsSuccess)
        {
            var result = await _guitarsUpdater.UpdateGuitarAsync(guitarDtoDto);
            return _resultMaker.ResolveResult(result);
        }

        return _resultMaker.ResolveResult(validationResult);
    }

    [HttpDelete("guitars/{id}/delete")]
    public async Task<ActionResult<ResultDto<int>>> Delete([FromRoute] int id)
    {
        var validationResult = await _guitarValidator.CheckIfGuitarExist(id);
        if (validationResult.IsSuccess)
        {
            var result = await _guitarsUpdater.DeleteGuitarAsync(id);
            return _resultMaker.ResolveResult(result);
        }

        return _resultMaker.ResolveResult(ResultCreator.GetInvalidResult<int>(
            Constants.ErrorMessages.InvalidGuitarId, HttpStatusCode.BadRequest));
    }
}