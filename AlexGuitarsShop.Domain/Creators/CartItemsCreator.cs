using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using GuitarDto = AlexGuitarsShop.Common.Models.Guitar;

namespace AlexGuitarsShop.Domain.Creators;

public class CartItemsCreator : ICartItemsCreator
{
    private readonly ICartItemRepository _cartItemRepository;
    
    public CartItemsCreator(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }
    
    public async Task AddNewCartItemAsync(GuitarDto guitar, int accountId)
    {
        Guitar guitarDal = guitar.ToGuitarDal() ?? throw new ArgumentNullException(nameof(guitar));
        CartItem item = _cartItemRepository.FindAsync(guitar.Id, accountId).Result;
        if (item == null)
        {
            item = new CartItem {Quantity = 1, Product = guitarDal};
            await _cartItemRepository.CreateAsync(item, accountId);
        }
    }
}