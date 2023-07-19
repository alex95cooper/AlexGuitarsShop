using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Enums;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Models;
using AlexGuitarsShop.Domain.Responses;
using AlexGuitarsShop.Service.Interfaces;

namespace AlexGuitarsShop.Service.Services;

public class CartItemService : ICartItemService
{
    private const int MinQuantity = 1;
    private const int MaxQuantity = 10;

    private readonly ICartItemRepository _cartItemRepository;

    public CartItemService(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task<IResponse<List<CartItem>>> GetCartItems()
    {
        var cartList = await _cartItemRepository.Select();
        if (cartList.Count == 0)
        {
            return new Response<List<CartItem>>
            {
                Description = "Cart is empty"
            };
        }

        return new Response<List<CartItem>>
        {
            Data = cartList,
            StatusCode = StatusCode.OK
        };
    }

    public async Task Remove(int id)
    {
        await _cartItemRepository.Delete(id);
    }

    public async Task Increment(int id)
    {
        int quantity = await _cartItemRepository.GetProductQuantity(id);
        quantity++;
        if (quantity <= MaxQuantity)
        {
            await _cartItemRepository.ChangeQuantity(id, quantity);
        }
    }

    public async Task Decrement(int id)
    {
        int quantity = await _cartItemRepository.GetProductQuantity(id);
        quantity--;
        if (quantity >= MinQuantity)
        {
            await _cartItemRepository.ChangeQuantity(id, quantity);
        }
    }

    public async Task Order()
    {
        await _cartItemRepository.DeleteAll();
    }
}