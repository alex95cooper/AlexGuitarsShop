using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.Validators;
using AlexGuitarsShop.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet(Constants.Routes.GetGuitars)]
    public async Task<ActionResult<Result<PaginatedListDto<GuitarDto>>>> Index(int pageNumber = 1)
    {
        int count = (await _guitarsProvider.GetCountAsync()).Data;
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
    
    [HttpGet(Constants.Routes.GetGuitar)]
    public async Task<ActionResult<Result<GuitarDto>>> Get(int id)
    {
        var validationResult = await _guitarValidator.CheckIfGuitarExist(id);
        if (validationResult.IsSuccess)
        {
            var result = await _guitarsProvider.GetGuitarAsync(id);
            return _resultMaker.ResolveResult(result);
        }

        return _resultMaker.ResolveResult(validationResult);
    }

    [HttpPost(Constants.Routes.Add)]
    public async Task<ActionResult<Result<GuitarDto>>> Add(GuitarDto guitarDtoDto)
    {
        var validationResult = _guitarValidator.CheckIfGuitarIsValid(guitarDtoDto);
        if (validationResult.IsSuccess)
        {
            var result = await _guitarsCreator.AddGuitarAsync(guitarDtoDto);
            return _resultMaker.ResolveResult(result);
        }
        
        return _resultMaker.ResolveResult(validationResult);
    }

    [HttpPut(Constants.Routes.UpdateGuitar)]
    public async Task<ActionResult<Result<GuitarDto>>> Update(GuitarDto guitarDtoDto)
    {
        var validationResult = await _guitarValidator.CheckIfGuitarUpdateIsValid(guitarDtoDto);
        if (validationResult.IsSuccess)
        {
            var result = await _guitarsUpdater.UpdateGuitarAsync(guitarDtoDto);
            return _resultMaker.ResolveResult(result);
        }
        
        return _resultMaker.ResolveResult(validationResult);
    }

    [HttpDelete(Constants.Routes.DeleteGuitar)]
    public async Task<ActionResult<Result<int>>> Delete(int id)
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