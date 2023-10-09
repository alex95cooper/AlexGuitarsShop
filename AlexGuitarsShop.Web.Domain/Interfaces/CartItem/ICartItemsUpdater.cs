using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Web.Domain.Interfaces.CartItem;

public interface ICartItemsUpdater
{
    Task<IResultDto<CartItemDto>> RemoveAsync(int id);

    Task<IResultDto<CartItemDto>> IncrementAsync(int id);

    Task<IResultDto<CartItemDto>> DecrementAsync(int id);

    Task<IResultDto<AccountDto>> OrderAsync();
}