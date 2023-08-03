using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.CartItem;

namespace AlexGuitarsShop.Domain.BLLClasses.Updaters;

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
        if (_cartItemRepository == null) throw new ArgumentNullException(nameof(_cartItemRepository));
        await _cartItemRepository.DeleteAsync(id)!;
    }

    public async Task IncrementAsync(int id)
    {
        if (_cartItemRepository == null) throw new ArgumentNullException(nameof(_cartItemRepository));
        int quantity = await _cartItemRepository.GetProductQuantityAsync(id)!;
        quantity++;
        if (quantity <= MaxQuantity)
        {
            await _cartItemRepository.UpdateQuantityAsync(id, quantity)!;
        }
    }

    public async Task DecrementAsync(int id)
    {
        if (_cartItemRepository == null) throw new ArgumentNullException(nameof(_cartItemRepository));
        int quantity = await _cartItemRepository.GetProductQuantityAsync(id)!;
        quantity--;
        if (quantity >= MinQuantity)
        {
            await _cartItemRepository.UpdateQuantityAsync(id, quantity)!;
        }
    }

    public async Task OrderAsync()
    {
        if (_cartItemRepository == null) throw new ArgumentNullException(nameof(_cartItemRepository));
        await _cartItemRepository.DeleteAllAsync()!;
    }
}