using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Domain.Creators;

public class CartItemsSessionCreator : ICartItemsSessionCreator
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartItemsSessionCreator(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;

    private string CartString
    {
        set => Context.Session.SetString(Constants.Cart.Key, value);
    }

    public void AddCartItem(Guitar guitar, List<CartItem> cart)
    {
        CartItem item = new() {Quantity = 1, Product = guitar};
        cart.Add(item);
        CartString = JsonConvert.SerializeObject(cart);
    }
}