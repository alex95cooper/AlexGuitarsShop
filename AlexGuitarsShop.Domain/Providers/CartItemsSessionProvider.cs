using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Domain.Providers;

public class CartItemsSessionProvider : ICartItemsSessionProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartItemsSessionProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;
    private string CartString => Context.Session.GetString(Constants.Cart.Key);

    public List<CartItem> GetCart()
    {
        return CartString == null
            ? new List<CartItem>()
            : JsonConvert.DeserializeObject<List<CartItem>>(CartString);
    }

    public CartItem GetCartItem(int id)
    {
        if (CartString == null)
        {
            return null;
        }

        List<CartItem> cart = JsonConvert.DeserializeObject<List<CartItem>>(CartString);
        return cart?.FirstOrDefault(item => item.Product.Id == id);
    }
}