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

    public async Task RemoveAsync(int id, int accountId)
    {
        await _cartItemRepository.DeleteAsync(id, accountId);
    }

    public async Task IncrementAsync(int id, int accountId)
    {
        int quantity = await _cartItemRepository.GetProductQuantityAsync(id, accountId) + 1;
        if (quantity <= MaxQuantity)
        {
            await _cartItemRepository.UpdateQuantityAsync(id, accountId, quantity);
        }
    }

    public async Task DecrementAsync(int id, int accountId)
    {
        int quantity = await _cartItemRepository.GetProductQuantityAsync(id, accountId) - 1;
        if (quantity >= MinQuantity)
        {
            await _cartItemRepository.UpdateQuantityAsync(id, accountId, quantity);
        }
    }

    public async Task OrderAsync(int accountId)
    {
        await _cartItemRepository.DeleteAllAsync(accountId);
    }
}