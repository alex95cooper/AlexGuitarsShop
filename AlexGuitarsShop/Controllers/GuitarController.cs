using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.Validators;
using AlexGuitarsShop.Extensions;
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
    private readonly ActionResultBuilder _resultBuilder;


    public GuitarController(IGuitarsCreator guitarsCreator,
        IGuitarsProvider guitarsProvider, IGuitarValidator guitarValidator, IGuitarsUpdater guitarsUpdater)
    {
        _guitarsCreator = guitarsCreator;
        _guitarsProvider = guitarsProvider;
        _guitarsUpdater = guitarsUpdater;
        _guitarValidator = guitarValidator;
        _resultBuilder = new ActionResultBuilder();
    }

    [HttpGet("guitars")]
    public async Task<ActionResult<ResultDto<PaginatedListDto<GuitarDto>>>> GetAll([FromQuery] int pageNumber = 1)
    {
        int count = (await _guitarsProvider.GetCountAsync()).Data;
        if (count == 0)
        {
            return _resultBuilder.ResolveResult(ResultCreator.GetValidResult(
                new PaginatedListDto<GuitarDto> {CountOfAll = 0, LimitedList = new List<GuitarDto>()},
                HttpStatusCode.NoContent));
        }

        if (!PageValidator.CheckIfPageIsValid(pageNumber, Paginator.Limit, count))
        {
            return _resultBuilder.ResolveResult(ResultCreator.GetInvalidResult<PaginatedListDto<GuitarDto>>(
                Constants.ErrorMessages.InvalidPage, HttpStatusCode.BadRequest));
        }

        int offset = Paginator.GetOffset(pageNumber);
        List<GuitarDto> list = (await _guitarsProvider.GetGuitarsByLimitAsync(offset, Paginator.Limit)).Data;
        //return _resultBuilder.ResolveResult(ResultCreator.GetValidResult(
        //    new PaginatedListDto<GuitarDto> {CountOfAll = count, LimitedList = list}, HttpStatusCode.OK));

        return this.ResolveResult(ResultCreator.GetValidResult(
            new PaginatedListDto<GuitarDto> {CountOfAll = count, LimitedList = list}, HttpStatusCode.OK));
    }

    [HttpGet("guitars/{id}")]
    public async Task<ActionResult<ResultDto<GuitarDto>>> Get([FromRoute] int id)
    {
        var validationResult = await _guitarValidator.CheckIfGuitarExist(id);
        if (validationResult.IsSuccess)
        {
            var result = await _guitarsProvider.GetGuitarAsync(id);
            return _resultBuilder.ResolveResult(result);
        }

        return _resultBuilder.ResolveResult(ResultCreator.GetInvalidResult<GuitarDto>(
            validationResult.Error, HttpStatusCode.BadRequest));
    }

    [HttpPost("guitars/add")]
    public async Task<ActionResult<ResultDto>> Add([FromBody] GuitarDto guitarDtoDto)
    {
        var validationResult = _guitarValidator.CheckIfGuitarIsValid(guitarDtoDto);
        if (validationResult.IsSuccess)
        {
            var result = await _guitarsCreator.AddGuitarAsync(guitarDtoDto);
            return _resultBuilder.ResolveResult(result);
        }

        return _resultBuilder.ResolveResult(validationResult);
    }

    [HttpPut("guitars/{id}/update")]
    public async Task<ActionResult<ResultDto>> Update([FromBody] GuitarDto guitarDtoDto)
    {
        var validationResult = await _guitarValidator.CheckIfGuitarUpdateIsValid(guitarDtoDto);
        if (validationResult.IsSuccess)
        {
            var result = await _guitarsUpdater.UpdateGuitarAsync(guitarDtoDto);
            return _resultBuilder.ResolveResult(result);
        }

        return _resultBuilder.ResolveResult(validationResult);
    }

    [HttpDelete("guitars/{id}/delete")]
    public async Task<ActionResult<ResultDto>> Delete([FromRoute] int id)
    {
        var validationResult = await _guitarValidator.CheckIfGuitarExist(id);
        if (validationResult.IsSuccess)
        {
            var result = await _guitarsUpdater.DeleteGuitarAsync(id);
            return _resultBuilder.ResolveResult(result);
        }

        return _resultBuilder.ResolveResult(ResultCreator.GetInvalidResult(
            Constants.ErrorMessages.InvalidGuitarId, HttpStatusCode.BadRequest));
    }
}