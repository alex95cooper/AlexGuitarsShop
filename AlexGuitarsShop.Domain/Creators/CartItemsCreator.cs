using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Domain.Creators;

public class CartItemsCreator : ICartItemsCreator
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartItemsCreator(ICartItemRepository cartItemRepository, IHttpContextAccessor httpContextAccessor)
    {
        _cartItemRepository = cartItemRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;

    private string CartString
    {
        get => Context.Session.GetString(Constants.Cart.Key);
        set => Context.Session.SetString(Constants.Cart.Key, value);
    }

    public async Task AddNewCartItemAsync(Guitar guitar, int accountId)
    {
        guitar = guitar ?? throw new ArgumentNullException(nameof(guitar));
        if (Context.User.Identity!.IsAuthenticated)
        {
            await AddToDbAsync(guitar, accountId);
        }
        else
        {
            AddToSession(guitar);
        }
    }

    private void AddToSession(Guitar guitar)
    {
        List<CartItem> cart = SessionCartProvider.GetCart(_httpContextAccessor);
        CartItem item = new() {Quantity = 1, Product = guitar};
        cart.Add(item);
        CartString = JsonConvert.SerializeObject(cart);
    }

    private async Task AddToDbAsync(Guitar guitar, int accountId)
    {
        CartItem item = _cartItemRepository.FindAsync(guitar.Id, accountId).Result;
        if (item == null)
        {
            item = new CartItem {Quantity = 1, Product = guitar};
            await _cartItemRepository.CreateAsync(item, accountId);
        }
    }

}