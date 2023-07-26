using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.CartItem;

namespace AlexGuitarsShop.Domain.EntityHandlers.CartItemsHandlers;

public class CartItemsUpdater : ICartItemsUpdater
{
    private const int MinQuantity = 1;
    private const int MaxQuantity = 10;

    private readonly ICartItemRepository _cartItemRepository;

    public CartItemsUpdater(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task RemoveAsync(int id)
    {
        await _cartItemRepository!.DeleteAsync(id)!;
    }

    public async Task IncrementAsync(int id)
    {
        int quantity = await _cartItemRepository!.GetProductQuantityAsync(id)!;
        quantity++;
        if (quantity <= MaxQuantity)
        {
            await _cartItemRepository!.ChangeQuantityAsync(id, quantity)!;
        }
    }

    public async Task DecrementAsync(int id)
    {
        int quantity = await _cartItemRepository!.GetProductQuantityAsync(id)!;
        quantity--;
        if (quantity >= MinQuantity)
        {
            await _cartItemRepository!.ChangeQuantityAsync(id, quantity)!;
        }
    }

    public async Task OrderAsync()
    {
        await _cartItemRepository!.DeleteAllAsync()!;
    }
}