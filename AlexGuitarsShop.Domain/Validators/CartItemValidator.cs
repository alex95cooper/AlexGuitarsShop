using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.CartItem;

namespace AlexGuitarsShop.Domain.Validators;

public class CartItemValidator : ICartItemValidator
{
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemValidator(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task<bool> CheckIfCartItemExist(int id, string cartId)
    {
        return await _cartItemRepository.FindAsync(id, cartId) != null;
    }
}