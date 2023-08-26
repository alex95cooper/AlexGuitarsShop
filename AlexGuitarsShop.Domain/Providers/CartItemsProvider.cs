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

    public async Task<IResult<CartItem>> GetCartItemAsync(int id, string cartId)
    {
        var item = await _cartItemRepository.FindAsync(id, cartId);
        return ResultCreator.GetValidResult(item);
    }

    public async Task<IResult<List<CartItem>>> GetCartItemsAsync(string cartId)
    {
        var cartList = await _cartItemRepository.GetAllAsync(cartId);
        return ResultCreator.GetValidResult(cartList);
    }
}