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
    private readonly ActionResultMaker _resultMaker;

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
        _resultMaker = new ActionResultMaker();
    }


    [HttpGet("carts")]
    public async Task<ActionResult<ResultDto<List<CartItemDto>>>> Get([FromQuery] string email)
    {
        var result = await _accountProvider.GetId(email);
        if (result.IsSuccess)
        {
            var cartResult = await _cartItemsProvider.GetCartAsync(result.Data);
            return _resultMaker.ResolveResult(cartResult);
        }

        return _resultMaker.ResolveResult(
            ResultCreator.GetInvalidResult<List<CartItemDto>>(
                Constants.ErrorMessages.InvalidEmail, HttpStatusCode.OK));
    }

    [HttpPost("carts/add")]
    public async Task<ActionResult<ResultDto<CartItemDto>>> Add([FromBody] CartItemDto item)
    {
        var result = GetAccountId(item.BuyerEmail);
        if (!result.IsSuccess)
        {
            return _resultMaker.ResolveResult(ResultCreator.GetInvalidResult<CartItemDto>(
                result.Error, HttpStatusCode.BadRequest));
        }

        var validationResult = await _guitarValidator.CheckIfGuitarExist(item.ProductId);
        if (!validationResult.IsSuccess)
        {
            return _resultMaker.ResolveResult(ResultCreator.GetInvalidResult<CartItemDto>(
                validationResult.Error, HttpStatusCode.BadRequest));
        }

        var guitarResult = await _guitarsProvider.GetReferenceGuitarAsync(item.ProductId);
        var cartResult = await _cartItemsCreator.AddNewCartItemAsync(guitarResult.Data, result.Data);
        return _resultMaker.ResolveResult(cartResult);
    }

    [HttpPut("carts/increment")]
    public async Task<ActionResult<ResultDto<CartItemDto>>> Increment([FromBody] CartItemDto item)
    {
        var validationResult = await ValidateUpdateRequest(item);
        if (validationResult.IsSuccess)
        {
            return _resultMaker.ResolveResult(
                await _cartItemsUpdater.IncrementAsync(item.ProductId,
                    validationResult.Data.BuyerId));
        }

        return _resultMaker.ResolveResult(validationResult);
    }

    [HttpPut("carts/decrement")]
    public async Task<ActionResult<ResultDto<CartItemDto>>> Decrement([FromBody] CartItemDto item)
    {
        var validationResult = await ValidateUpdateRequest(item);
        if (validationResult.IsSuccess)
        {
            return _resultMaker.ResolveResult(
                await _cartItemsUpdater.DecrementAsync(item.ProductId,
                    validationResult.Data.BuyerId));
        }

        return _resultMaker.ResolveResult(validationResult);
    }

    [HttpDelete("carts/delete")]
    public async Task<ActionResult<ResultDto<int>>> Remove([FromQuery] int id, [FromQuery] string email)
    {
        CartItemDto item = new() {ProductId = id, BuyerEmail = email};
        var validationResult = await ValidateUpdateRequest(item);
        if (validationResult.IsSuccess)
        {
            return _resultMaker.ResolveResult(
                await _cartItemsUpdater.RemoveAsync(item.ProductId,
                    validationResult.Data.BuyerId));
        }

        return _resultMaker.ResolveResult(validationResult);
    }

    [HttpPut("carts/order")]
    public async Task Order([FromBody] AccountDto accountDto)
    {
        var result = GetAccountId(accountDto.Email);
        if (result.IsSuccess)
        {
            await _cartItemsUpdater.OrderAsync(result.Data);
        }
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