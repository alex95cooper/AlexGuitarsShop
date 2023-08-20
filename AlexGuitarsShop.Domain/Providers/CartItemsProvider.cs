using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.CartItem;

namespace AlexGuitarsShop.Domain.Providers;

public class CartItemsProvider : ICartItemsProvider
{
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemsProvider(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task<IResult<CartItem>> GetCartItemAsync(int id)
    {
        var item = await _cartItemRepository.FindAsync(id);
        return ResultCreator.GetValidResult(item);
    }

    public async Task<IResult<List<CartItem>>> GetCartItemsAsync()
    {
        var cartList = await _cartItemRepository.GetAllAsync();
        return ResultCreator.GetValidResult(cartList);
    }
}