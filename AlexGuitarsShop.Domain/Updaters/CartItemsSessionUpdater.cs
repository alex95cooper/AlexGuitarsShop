using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Domain.Updaters;

public class CartItemsSessionUpdater : ICartItemsSessionUpdater
{
    private const int BuyLimit = 10;
    
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartItemsSessionUpdater(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;
    private string CartString
    {
        set => Context.Session.SetString(Constants.Cart.Key, value);
    }
    
    public void Remove(int id, List<CartItem> cart)
    {
        foreach (var cartItem in cart.Where(cartItem => cartItem.Product.Id == id))
        {
            cart.Remove(cartItem);
            CartString = JsonConvert.SerializeObject(cart);
            return;
        }
    }
    
    public void Increment(int id, List<CartItem> cart)
    {
        foreach (var cartItem in cart.Where(cartItem => cartItem.Product.Id == id))
        {
            if (cartItem.Quantity < BuyLimit)
            {
                int quantity = cartItem.Quantity;
                cartItem.Quantity = quantity + 1;
            }
        }

        CartString = JsonConvert.SerializeObject(cart);
    }

    public void Decrement(int id, List<CartItem> cart)
    {
        foreach (var cartItem in cart.Where(cartItem => cartItem.Product.Id == id))
        {
            if (cartItem.Quantity > 1)
            {
                int quantity = cartItem.Quantity;
                cartItem.Quantity = quantity - 1;
            }
        }

        CartString = JsonConvert.SerializeObject(cart);
    }

    public void Order()
    {
        CartString = null;
    }
}