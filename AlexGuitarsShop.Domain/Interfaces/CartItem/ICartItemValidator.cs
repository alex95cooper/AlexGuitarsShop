using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemValidator
{
    Task<IResult<CartItemDto>> CheckIfCartItemExist(int id, int accountId);
}