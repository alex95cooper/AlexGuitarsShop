using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

[ApiController]
public class CartController : Controller
{
    private readonly ICartItemsCreator _cartItemsCreator;
    private readonly ICartItemsProvider _cartItemsProvider;
    private readonly ICartItemsUpdater _cartItemsUpdater;
    private readonly IGuitarsProvider _guitarsProvider;
    private readonly IAccountsProvider _accountProvider;
    private readonly ICartItemValidator _cartItemValidator;
    private readonly IGuitarValidator _guitarValidator;
    private readonly ActionResultBuilder _resultBuilder;

    public CartController(ICartItemsCreator cartItemsCreator, ICartItemsProvider cartItemsProvider,
        ICartItemsUpdater cartItemsUpdater, IGuitarsProvider guitarsProvider, IAccountsProvider accountProvider,
        ICartItemValidator cartItemValidator, IGuitarValidator guitarValidator)
    {
        _cartItemsCreator = cartItemsCreator;
        _cartItemsProvider = cartItemsProvider;
        _cartItemsUpdater = cartItemsUpdater;
        _guitarsProvider = guitarsProvider;
        _accountProvider = accountProvider;
        _cartItemValidator = cartItemValidator;
        _guitarValidator = guitarValidator;
        _resultBuilder = new ActionResultBuilder();
    }


    [HttpGet("carts")]
    public async Task<ActionResult<ResultDto<List<CartItemDto>>>> Get([FromQuery] string email)
    {
        var result = await _accountProvider.GetId(email);
        if (result.IsSuccess)
        {
            var cartResult = await _cartItemsProvider.GetCartAsync(result.Data);
            return _resultBuilder.ResolveResult(cartResult);
        }

        return _resultBuilder.ResolveResult(
            ResultCreator.GetInvalidResult<List<CartItemDto>>(
                Constants.ErrorMessages.InvalidEmail, HttpStatusCode.OK));
    }

    [HttpPost("carts/add-product")]
    public async Task<ActionResult<ResultDto>> Add([FromBody] CartItemDto item)
    {
        var result = GetAccountId(item.BuyerEmail);
        if (!result.IsSuccess)
        {
            return _resultBuilder.ResolveResult(ResultCreator.GetInvalidResult(
                result.Error, HttpStatusCode.BadRequest));
        }

        var validationResult = await _guitarValidator.CheckIfGuitarExist(item.ProductId);
        if (!validationResult.IsSuccess)
        {
            return _resultBuilder.ResolveResult(ResultCreator.GetInvalidResult(
                validationResult.Error, HttpStatusCode.BadRequest));
        }

        var guitarResult = await _guitarsProvider.GetReferenceGuitarAsync(item.ProductId);
        var cartResult = await _cartItemsCreator.AddNewCartItemAsync(guitarResult.Data, result.Data);
        return _resultBuilder.ResolveResult(cartResult);
    }

    [HttpPut("carts/increment-product")]
    public async Task<ActionResult<ResultDto>> Increment([FromBody] CartItemDto item)
    {
        var validationResult = await ValidateUpdateRequest(item);
        if (validationResult.IsSuccess)
        {
            return _resultBuilder.ResolveResult(
                await _cartItemsUpdater.IncrementAsync(item.ProductId,
                    validationResult.Data.BuyerId));
        }

        return _resultBuilder.ResolveResult(ResultCreator.GetInvalidResult(
            validationResult.Error, HttpStatusCode.BadRequest));
    }

    [HttpPut("carts/decrement-product")]
    public async Task<ActionResult<ResultDto>> Decrement([FromBody] CartItemDto item)
    {
        var validationResult = await ValidateUpdateRequest(item);
        if (validationResult.IsSuccess)
        {
            return _resultBuilder.ResolveResult(
                await _cartItemsUpdater.DecrementAsync(item.ProductId,
                    validationResult.Data.BuyerId));
        }

        return _resultBuilder.ResolveResult(ResultCreator.GetInvalidResult(
            validationResult.Error, HttpStatusCode.BadRequest));
    }

    [HttpPut("carts/delete-product")]
    public async Task<ActionResult<ResultDto>> Remove([FromBody] CartItemDto item)
    {
        var validationResult = await ValidateUpdateRequest(item);
        if (validationResult.IsSuccess)
        {
            return _resultBuilder.ResolveResult(
                await _cartItemsUpdater.RemoveAsync(item.ProductId,
                    validationResult.Data.BuyerId));
        }

        return _resultBuilder.ResolveResult(ResultCreator.GetInvalidResult(
            validationResult.Error, HttpStatusCode.BadRequest));
    }

    [HttpPut("carts/make-order")]
    public async Task<ActionResult<ResultDto>> Order([FromBody] AccountDto accountDto)
    {
        var result = GetAccountId(accountDto.Email);
        if (result.IsSuccess)
        {
            await _cartItemsUpdater.OrderAsync(result.Data);
        }

        return _resultBuilder.ResolveResult(ResultCreator.GetValidResult());
    }

    private IResult<int> GetAccountId(string email)
    {
        return _accountProvider.GetId(email).Result;
    }

    private async Task<IResult<CartItemDto>> ValidateUpdateRequest(CartItemDto item)
    {
        var result = GetAccountId(item.BuyerEmail);
        if (!result.IsSuccess)
        {
            return ResultCreator.GetInvalidResult<CartItemDto>(result.Error, HttpStatusCode.BadRequest);
        }

        return await _cartItemValidator.CheckIfCartItemExist(item.ProductId, result.Data);
    }
}