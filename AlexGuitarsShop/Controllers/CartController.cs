using AlexGuitarsShop.Common;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using Microsoft.AspNetCore.Mvc;
using CartItemDto = AlexGuitarsShop.Common.Models.CartItem;

namespace AlexGuitarsShop.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : Controller
{
    private readonly ICartItemsCreator _cartItemsCreator;
    private readonly ICartItemsProvider _cartItemsProvider;
    private readonly ICartItemsUpdater _cartItemsUpdater;
    private readonly IGuitarsProvider _guitarsProvider;
    private readonly IAccountsProvider _accountProvider;
    private readonly ICartItemValidator _cartItemValidator;
    private readonly IGuitarValidator _guitarValidator;

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
    }

    [HttpGet("Index/{email}")]
    public IResult<List<CartItemDto>> Index(string email)
    {
        var result = _accountProvider.GetId(email).Result;
        if (result.IsSuccess)
        {
            List<CartItemDto> cart = _cartItemsProvider.GetCartAsync(result.Data).Result.Data;
            return ResultCreator.GetValidResult(cart);
        }
        
        return ResultCreator.GetInvalidResult<List<CartItemDto>>(Constants.ErrorMessages.InvalidEmail);
    }

    [HttpGet("Add/id={id}&email={email}")]
    public async Task<IResult<int>> Add(int id, string email)
    {
        var result = GetAccountId(email);
        if (!result.IsSuccess)
        {
            return ResultCreator.GetInvalidResult<int>(result.Error);
        }
        
        if (!_guitarValidator.CheckIfGuitarExist(id).Result)
        {
            return ResultCreator.GetInvalidResult<int>(Constants.ErrorMessages.InvalidGuitarId);
        }

        var guitarResult = await _guitarsProvider.GetGuitarAsync(id);
        await _cartItemsCreator.AddNewCartItemAsync(guitarResult.Data, result.Data);
        return ResultCreator.GetValidResult(id);
    }

    [HttpGet("Remove/id={id}&email={email}")]
    public async Task<IResult<int>> Remove(int id, string email)
    {
        var result = ValidateUpdateRequest(id, email).Result;
        if (result.IsSuccess)
        {
            await _cartItemsUpdater.RemoveAsync(id, result.Data);
        }

        return result;
    }

    [HttpGet("Increment/id={id}&email={email}")]
    public async Task<IResult<int>> Increment(int id, string email)
    {
        var result = ValidateUpdateRequest(id, email).Result;
        if (result.IsSuccess)
        {
            await _cartItemsUpdater.IncrementAsync(id, result.Data);
        }

        return result;
    }

    [HttpGet("Decrement/id={id}&email={email}")]
    public async Task<IResult<int>> Decrement(int id, string email)
    {
        var result = ValidateUpdateRequest(id, email).Result;
        if (result.IsSuccess)
        {
            await _cartItemsUpdater.DecrementAsync(id, result.Data);
        }

        return result;
    }

    [HttpGet("Order/{email}")]
    public async Task Order(string email)
    {
        var result = GetAccountId(email);
        if (result.IsSuccess)
        {
            await _cartItemsUpdater.OrderAsync(result.Data);
        }
    }
    
    private IResult<int> GetAccountId(string email)
    {
        return _accountProvider.GetId(email).Result;
    }

    private async Task<bool> CheckIfDbCartItemExist(int id, int accountId)
    {
        return !await _cartItemValidator.CheckIfCartItemExist(id, accountId);
    }
    
    private async Task<IResult<int>> ValidateUpdateRequest(int id, string email)
    {
        var result = GetAccountId(email);
        if (!result.IsSuccess)
        {
            return ResultCreator.GetInvalidResult<int>(result.Error);
        }
        
        if (await CheckIfDbCartItemExist(id, result.Data))
        {
            return ResultCreator.GetInvalidResult<int>(Constants.ErrorMessages.InvalidProductId);
        }

        return ResultCreator.GetValidResult(result.Data);
    }
}