using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.CartItem;

namespace AlexGuitarsShop.Domain.Updaters;

public class CartItemsUpdater : ICartItemsUpdater
{
    private const int MinQuantity = 1;
    private const int MaxQuantity = 10;

    private readonly ICartItemRepository _cartItemRepository;

    public CartItemsUpdater(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task RemoveAsync(int id, string cartId)
    {
        await _cartItemRepository.DeleteAsync(id, cartId);
    }

    public async Task IncrementAsync(int id, string cartId)
    {
        int quantity = await _cartItemRepository.GetProductQuantityAsync(id, cartId);
        quantity++;
        if (quantity <= MaxQuantity)
        {
            await _cartItemRepository.UpdateQuantityAsync(id, quantity, cartId);
        }
    }

    public async Task DecrementAsync(int id, string cartId)
    {
        int quantity = await _cartItemRepository.GetProductQuantityAsync(id, cartId);
        quantity--;
        if (quantity >= MinQuantity)
        {
            await _cartItemRepository.UpdateQuantityAsync(id, quantity, cartId);
        }
    }

    public async Task OrderAsync(string cartId)
    {
        await _cartItemRepository.DeleteAllAsync(cartId);
    }
}