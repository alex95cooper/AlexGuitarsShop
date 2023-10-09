using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Interfaces.CartItem;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain.Updaters;

public class CartItemsUpdater : ICartItemsUpdater
{
    private const int MinQuantity = 1;
    private const int MaxQuantity = 10;

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IShopBackendService _shopBackendService;

    public CartItemsUpdater(IShopBackendService shopBackendService,
        IHttpContextAccessor httpContextAccessor)
    {
        _shopBackendService = shopBackendService;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;

    private string CartString
    {
        set
        {
            if (value == null)
            {
                Context.Session.Remove(Constants.Cart.Key);
            }
            else
            {
                Context.Session.SetString(Constants.Cart.Key, value);
            }
        }
    }

    public async Task<IResultDto<CartItemDto>> RemoveAsync(int id)
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            CartItemDto item = new() {ProductId = id, BuyerEmail = Context.User.Identity.Name};
            return await _shopBackendService.PutAsync(item,Constants.Routes.DeleteCartItem);
        }

        return RemoveFromSessionCart(id);
    }

    public async Task<IResultDto<CartItemDto>> IncrementAsync(int id)
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            CartItemDto item = new() {ProductId = id, BuyerEmail = Context.User.Identity.Name};
            return await _shopBackendService.PutAsync(item, Constants.Routes.Increment);
        }

        return IncrementSessionCart(id);
    }

    public async Task<IResultDto<CartItemDto>> DecrementAsync(int id)
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            CartItemDto item = new() {ProductId = id, BuyerEmail = Context.User.Identity.Name};
            return await _shopBackendService.PutAsync(item, Constants.Routes.Decrement);
        }

        return DecrementSessionCart(id);
    }

    public async Task<IResultDto<AccountDto>> OrderAsync()
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            AccountDto account = new AccountDto {Email = Context.User.Identity.Name};
            return await _shopBackendService.PutAsync(account, Constants.Routes.Order);
        }

        CartString = null;
        return ResultDtoCreator.GetValidResult(new AccountDto());
    }

    private IResultDto<CartItemDto> RemoveFromSessionCart(int id)
    {
        List<CartItemDto> cart = SessionCartProvider.GetCart(_httpContextAccessor);
        foreach (var cartItem in cart.Where(cartItem => cartItem.Product.Id == id))
        {
            cart.Remove(cartItem);
            CartString = JsonConvert.SerializeObject(cart);
            return ResultDtoCreator.GetValidResult(new CartItemDto());
        }

        return ResultDtoCreator.GetInvalidResult<CartItemDto>(Constants.Cart.ItemNotExist);
    }

    private IResultDto<CartItemDto> IncrementSessionCart(int id)
    {
        List<CartItemDto> cart = SessionCartProvider.GetCart(_httpContextAccessor);
        foreach (var cartItem in cart.Where(cartItem => cartItem.Product.Id == id))
        {
            if (cartItem.Quantity < MaxQuantity)
            {
                int quantity = cartItem.Quantity;
                cartItem.Quantity = quantity + 1;
            }

            CartString = JsonConvert.SerializeObject(cart);
            return ResultDtoCreator.GetValidResult(cartItem);
        }

        CartString = JsonConvert.SerializeObject(cart);
        return ResultDtoCreator.GetInvalidResult<CartItemDto>(Constants.Cart.ItemNotExist);
    }

    private ResultDto<CartItemDto> DecrementSessionCart(int id)
    {
        List<CartItemDto> cart = SessionCartProvider.GetCart(_httpContextAccessor);
        foreach (var cartItem in cart.Where(cartItem => cartItem.Product.Id == id))
        {
            if (cartItem.Quantity > MinQuantity)
            {
                int quantity = cartItem.Quantity;
                cartItem.Quantity = quantity - 1;
            }

            CartString = JsonConvert.SerializeObject(cart);
            return ResultDtoCreator.GetValidResult(cartItem);
        }

        CartString = JsonConvert.SerializeObject(cart);
        return ResultDtoCreator.GetInvalidResult<CartItemDto>(
            Constants.Cart.ItemNotExist);
    }
}