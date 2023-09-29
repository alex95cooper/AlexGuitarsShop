using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Web.Domain.Interfaces.CartItem;

public interface ICartItemsUpdater
{
    Task<IResult<int>> RemoveAsync(int id);

    Task<IResult<CartItemDto>> IncrementAsync(int id);

    Task<IResult<CartItemDto>> DecrementAsync(int id);

    Task<IResult<int>> OrderAsync();
}