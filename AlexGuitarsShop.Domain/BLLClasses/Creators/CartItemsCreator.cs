using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.BLLClasses.Creators;

public class CartItemsCreator : ICartItemsCreator
{
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemsCreator(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository
                              ?? throw new ArgumentNullException(nameof(cartItemRepository));
    }
    
    public async Task AddNewCartItemAsync(GuitarViewModel model)
    {
        model = model ?? throw new ArgumentNullException(nameof(model));
        Guitar product = model.ToGuitar();
        product = product ?? throw new ArgumentNullException(nameof(product));
        CartItem item = new CartItem {Quantity = 1, Product = product};

                await _cartItemRepository.CreateAsync(item)!;
            
        
    }

    public async Task AddCartItemAsync(CartItem item)
    {
        await _cartItemRepository.CreateAsync(item)!;
    }
}