using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces.CartItem;

namespace AlexGuitarsShop.Domain.Creators;

public class CartItemsCreator : ICartItemsCreator
{
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemsCreator(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task AddNewCartItemAsync(Guitar guitar, int accountId)
    {
        guitar = guitar ?? throw new ArgumentNullException(nameof(guitar));
        CartItem item = _cartItemRepository.FindAsync(guitar.Id, accountId).Result;
        if (item == null)
        {
            item =  new CartItem {Quantity = 1, Product = guitar};
            await _cartItemRepository.CreateAsync(item, accountId);
        }
    }
}