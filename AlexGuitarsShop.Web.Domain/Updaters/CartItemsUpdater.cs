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

    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartItemsUpdater(HttpClient client,
        IHttpContextAccessor httpContextAccessor)
    {
        _client = client;
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
            var response = await _client.GetAsync(
                $"http://localhost:5001/Cart/Remove/id={id}&email={Context.User.Identity.Name}");
            return JsonConvert.DeserializeObject<Result<int>>(await response.Content
                .ReadAsStringAsync());
        }

        return RemoveFromSessionCart(id);
    }

    public async Task<IResult<int>> IncrementAsync(int id)
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            var response = await _client.GetAsync(
                $"http://localhost:5001/Cart/Increment/id={id}&email={Context.User.Identity.Name}");
            return JsonConvert.DeserializeObject<Result<int>>(await response.Content
                .ReadAsStringAsync());
        }

        return IncrementSessionCart(id);
    }

    public async Task<IResult<int>> DecrementAsync(int id)
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            var response = await _client.GetAsync(
                $"http://localhost:5001/Cart/Decrement/id={id}&email={Context.User.Identity.Name}");
            return JsonConvert.DeserializeObject<Result<int>>(await response.Content
                .ReadAsStringAsync());
        }

        return DecrementSessionCart(id);
    }

    public async Task OrderAsync()
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            await _client.GetAsync(
                $"http://localhost:5001/Cart/Order/{Context.User.Identity.Name}");
        }
        else
        {
            CartString = null;
        }
    }

    private IResult<int> RemoveFromSessionCart(int id)
    {
        List<CartItem> cart = SessionCartProvider.GetCart(_httpContextAccessor);
        foreach (var cartItem in cart.Where(cartItem => cartItem.Product.Id == id))
        {
            cart.Remove(cartItem);
            CartString = JsonConvert.SerializeObject(cart);
            return ResultCreator.GetValidResult(id);
        }

        return ResultCreator.GetInvalidResult<int>(Constants.Cart.ItemNotExist);
    }

    private IResult<int> IncrementSessionCart(int id)
    {
        List<CartItem> cart = SessionCartProvider.GetCart(_httpContextAccessor);
        foreach (var cartItem in cart.Where(cartItem => cartItem.Product.Id == id))
        {
            if (cartItem.Quantity < MaxQuantity)
            {
                int quantity = cartItem.Quantity;
                cartItem.Quantity = quantity + 1;
            }

            CartString = JsonConvert.SerializeObject(cart);
            return ResultCreator.GetValidResult(id);
        }

        CartString = JsonConvert.SerializeObject(cart);
        return ResultCreator.GetInvalidResult<int>(Constants.Cart.ItemNotExist);
    }

    private IResult<int> DecrementSessionCart(int id)
    {
        List<CartItem> cart = SessionCartProvider.GetCart(_httpContextAccessor);
        foreach (var cartItem in cart.Where(cartItem => cartItem.Product.Id == id))
        {
            if (cartItem.Quantity > MinQuantity)
            {
                int quantity = cartItem.Quantity;
                cartItem.Quantity = quantity - 1;
            }

            CartString = JsonConvert.SerializeObject(cart);
            return ResultCreator.GetValidResult(id);
        }

        CartString = JsonConvert.SerializeObject(cart);
        return ResultCreator.GetInvalidResult<int>(Constants.Cart.ItemNotExist);
    }
}