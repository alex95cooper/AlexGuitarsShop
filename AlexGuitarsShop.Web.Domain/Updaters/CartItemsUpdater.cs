using System.Net;
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
    private readonly IResponseMaker _responseMaker;

    public CartItemsUpdater(IResponseMaker responseMaker,
        IHttpContextAccessor httpContextAccessor)
    {
        _responseMaker = responseMaker;
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

    public async Task<IResult<int>> RemoveAsync(int id)
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            return await _responseMaker.DeleteAsync(
                string.Format(Constants.Routes.DeleteCartItem, id, Context.User.Identity.Name));
        }

        return RemoveFromSessionCart(id);
    }

    public async Task<IResult<CartItemDto>> IncrementAsync(int id)
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            CartItemDto item = new() {ProductId = id, BuyerEmail = Context.User.Identity.Name};
            return await _responseMaker.PutAsync(item, Constants.Routes.Increment);
        }

        return IncrementSessionCart(id);
    }

    public async Task<IResult<CartItemDto>> DecrementAsync(int id)
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            CartItemDto item = new() {ProductId = id, BuyerEmail = Context.User.Identity.Name};
            return await _responseMaker.PutAsync(item, Constants.Routes.Decrement);
        }

        return DecrementSessionCart(id);
    }

    public async Task<IResult<int>> OrderAsync()
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            return await _responseMaker.DeleteAsync(
                string.Format(Constants.Routes.Order, Context.User.Identity.Name));
        }
        else
        {
            CartString = null;
            return ResultCreator.GetValidResult(0, HttpStatusCode.OK);
        }
    }

    private IResult<int> RemoveFromSessionCart(int id)
    {
        List<CartItemDto> cart = SessionCartProvider.GetCart(_httpContextAccessor);
        foreach (var cartItem in cart.Where(cartItem => cartItem.Product.Id == id))
        {
            cart.Remove(cartItem);
            CartString = JsonConvert.SerializeObject(cart);
            return ResultCreator.GetValidResult(id, HttpStatusCode.OK);
        }

        return ResultCreator.GetInvalidResult<int>(
            Constants.Cart.ItemNotExist, HttpStatusCode.BadRequest);
    }

    private IResult<CartItemDto> IncrementSessionCart(int id)
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
            return ResultCreator.GetValidResult(cartItem, HttpStatusCode.OK);
        }

        CartString = JsonConvert.SerializeObject(cart);
        return ResultCreator.GetInvalidResult<CartItemDto>(
            Constants.Cart.ItemNotExist, HttpStatusCode.BadRequest);
    }

    private Result<CartItemDto> DecrementSessionCart(int id)
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
            return ResultCreator.GetValidResult(cartItem, HttpStatusCode.OK);
        }

        CartString = JsonConvert.SerializeObject(cart);
        return ResultCreator.GetInvalidResult<CartItemDto>(
            Constants.Cart.ItemNotExist, HttpStatusCode.BadRequest);
    }
}