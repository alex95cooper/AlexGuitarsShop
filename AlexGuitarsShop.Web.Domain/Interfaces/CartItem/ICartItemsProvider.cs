using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Web.Domain.Interfaces.CartItem;

public interface ICartItemsProvider
{
    Task<IResultDto<CartItemDto>> GetCartItemAsync(int id);

    Task<IResultDto<List<CartItemDto>>> GetCartAsync();
}