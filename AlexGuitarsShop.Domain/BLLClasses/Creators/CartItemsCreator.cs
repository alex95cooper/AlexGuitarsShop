using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces.CartItem;

namespace AlexGuitarsShop.Domain.BLLClasses.Creators;

public class CartItemsCreator : ICartItemsCreator
{
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemsCreator(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task AddCartItemAsync(CartItem item)
    {
        if (_cartItemRepository == null) throw new ArgumentNullException(nameof(_cartItemRepository));
        await _cartItemRepository.CreateAsync(item)!;
    }
}