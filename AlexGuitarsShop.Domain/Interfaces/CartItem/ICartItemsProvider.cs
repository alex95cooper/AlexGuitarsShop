using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsProvider
{
    Task<IResult<List<CartItemDto>>> GetCartAsync(int accountId);
}