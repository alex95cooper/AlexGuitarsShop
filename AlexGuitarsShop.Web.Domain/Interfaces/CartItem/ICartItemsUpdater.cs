using AlexGuitarsShop.Common;
using CartItemDto = AlexGuitarsShop.Common.Models.CartItem;

namespace AlexGuitarsShop.Web.Domain.Interfaces.CartItem;

public interface ICartItemsUpdater
{
    Task<IResult<int>> RemoveAsync(int id);

    Task<IResult<int>> IncrementAsync(int id);

    Task<IResult<int>> DecrementAsync(int id);

    Task OrderAsync();
}