using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Domain.Providers;

public class CartItemsProvider : ICartItemsProvider
{
    
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartItemsProvider(IHttpContextAccessor httpContextAccessor, 
        ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    
    private HttpContext Context => _httpContextAccessor.HttpContext;
    private string CartString => Context.Session.GetString(Constants.Cart.Key);

    public async Task<IResult<CartItem>> GetCartItemAsync(int id, int accountId)
    {
        CartItem item = Context.User.Identity!.IsAuthenticated 
            ? await _cartItemRepository.FindAsync(id, accountId) 
            : GetCartItem(id);
        return ResultCreator.GetValidResult(item);
    }

    public async Task<IResult<List<CartItem>>> GetCartAsync(int accountId)
    {
        List<CartItem> cart = Context.User.Identity!.IsAuthenticated 
            ? await _cartItemRepository.GetAllAsync(accountId)
            : SessionCartProvider.GetCart(_httpContextAccessor);
        return ResultCreator.GetValidResult(cart);
    }
    
    private CartItem GetCartItem(int id)
    {
        if (CartString == null)
        {
            return null;
        }

        List<CartItem> cart = JsonConvert.DeserializeObject<List<CartItem>>(CartString);
        return cart?.FirstOrDefault(item => item.Product.Id == id);
    }
}