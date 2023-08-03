using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.CartItem;

namespace AlexGuitarsShop.Domain.BLLClasses.Providers;

public class CartItemsProvider : ICartItemsProvider
{
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemsProvider(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task<IResult<CartItem>> GetCartItemAsync(int id)
    {
        if (_cartItemRepository == null) throw new ArgumentNullException(nameof(_cartItemRepository));
        var item = await _cartItemRepository.FindAsync(id)!;
        return ResultCreator.GetValidResult(item);
    }

    public async Task<IResult<List<CartItem>>> GetCartItemsAsync()
    {
        if (_cartItemRepository == null) throw new ArgumentNullException(nameof(_cartItemRepository));
        var cartList = await _cartItemRepository.GetAllAsync()!;
        return ResultCreator.GetValidResult(cartList);
    }
}