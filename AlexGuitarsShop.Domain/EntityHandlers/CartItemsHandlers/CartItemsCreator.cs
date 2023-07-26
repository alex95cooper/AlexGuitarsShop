using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces.CartItem;

namespace AlexGuitarsShop.Domain.EntityHandlers.CartItemsHandlers;

public class CartItemsCreator : ICartItemsCreator
{
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemsCreator(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task AddCartItemAsync(CartItem item)
    {
        await _cartItemRepository!.AddAsync(item)!;
    }
}