using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.CartItem;

namespace AlexGuitarsShop.Domain.EntityHandlers.CartItemsHandlers;

public class CartItemsProvider : ICartItemsProvider
{

    private const string ItemAlreadyAddedMessage = "Item already added";
    private const string CartEmptyMessage = "Cart is empty";
    
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemsProvider(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task<IResponse<CartItem>> GetCartItemAsync(int id)
    {
        try
        {
            var item = await _cartItemRepository!.GetAsync(id)!;
            return ResponseCreator.GetValidResponse(item);
        }
        catch (Exception e)
        {
            return ResponseCreator.GetInvalidResponse<CartItem>(ItemAlreadyAddedMessage);
        }
    }

    public async Task<IResponse<List<CartItem>>> GetCartItemsAsync()
    {
        var cartList = await _cartItemRepository!.SelectAsync()!;
        return cartList!.Count == 0 
            ? ResponseCreator.GetInvalidResponse<List<CartItem>>(CartEmptyMessage) 
            : ResponseCreator.GetValidResponse(cartList);
    }
}