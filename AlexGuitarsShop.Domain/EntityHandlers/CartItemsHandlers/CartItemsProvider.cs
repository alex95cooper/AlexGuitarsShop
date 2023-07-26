using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.CartItem;

namespace AlexGuitarsShop.Domain.EntityHandlers.CartItemsHandlers;

public class CartItemsProvider : ICartItemsProvider
{
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemsProvider(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task<IResponse<List<CartItem>>> GetCartItemsAsync()
    {
        var cartList = await _cartItemRepository!.SelectAsync()!;
        if (cartList!.Count == 0)
        {
            return new Response<List<CartItem>>
            {
                Description = "Cart is empty"
            };
        }

        return new Response<List<CartItem>>
        {
            Data = cartList,
            StatusCode = StatusCode.Ok
        };
    }
}