using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsUpdater
{
    Task<IResult<int>> RemoveAsync(int id, int accountId);

    Task<IResult<CartItemDto>> IncrementAsync(int id, int accountId);

    Task<IResult<CartItemDto>> DecrementAsync(int id, int accountId);

    Task<IResult<string>> OrderAsync(int accountId);
}