using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Domain.Updaters;

public class CartItemsUpdater : ICartItemsUpdater
{
    private const int MinQuantity = 1;
    private const int MaxQuantity = 10;

    private readonly ICartItemRepository _cartItemRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartItemsUpdater(ICartItemRepository cartItemRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _cartItemRepository = cartItemRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    
    private HttpContext Context => _httpContextAccessor.HttpContext;
    private string CartString
    {
        set => Context.Session.SetString(Constants.Cart.Key, value);
        get => Context.Session.GetString(Constants.Cart.Key);
    }

    public async Task RemoveAsync(int id, int accountId)
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            await _cartItemRepository.DeleteAsync(id, accountId);
        }
        else
        {
            RemoveFromSessionCart(id);
        }
    }

    public async Task IncrementAsync(int id, int accountId)
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            int quantity = await _cartItemRepository.GetProductQuantityAsync(id, accountId) + 1;
            if (quantity <= MaxQuantity)
            {
                await _cartItemRepository.UpdateQuantityAsync(id, accountId, quantity);
            }
        }
        else
        {
            IncrementSessionCart(id);
        }
    }

    public async Task DecrementAsync(int id, int accountId)
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            int quantity = await _cartItemRepository.GetProductQuantityAsync(id, accountId) - 1;
            if (quantity >= MinQuantity)
            {
                await _cartItemRepository.UpdateQuantityAsync(id, accountId, quantity);
            }
        }
        else
        {
            DecrementSessionCart(id);
        }
    }

    public async Task OrderAsync(int accountId)
    {
        if (Context.User.Identity!.IsAuthenticated)
        {
            await _cartItemRepository.DeleteAllAsync(accountId);
        }
        else
        {
            CartString = null;
        }
    }

    private void RemoveFromSessionCart(int id)
    {
        List<CartItem> cart = SessionCartProvider.GetCart(_httpContextAccessor);
        foreach (var cartItem in cart.Where(cartItem => cartItem.Product.Id == id))
        {
            cart.Remove(cartItem);
            CartString = JsonConvert.SerializeObject(cart);
            return;
        }
    }

    private void IncrementSessionCart(int id)
    {
        List<CartItem> cart = SessionCartProvider.GetCart(_httpContextAccessor);
        foreach (var cartItem in cart.Where(cartItem => cartItem.Product.Id == id))
        {
            if (cartItem.Quantity < MaxQuantity)
            {
                int quantity = cartItem.Quantity;
                cartItem.Quantity = quantity + 1;
            }
        }

        CartString = JsonConvert.SerializeObject(cart);
    }
    
    private void DecrementSessionCart(int id)
    {
        List<CartItem> cart = SessionCartProvider.GetCart(_httpContextAccessor);
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
}