using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Web.Domain.Interfaces.CartItem;

public interface ICartItemsProvider
{
    Task<IResult<CartItemDto>> GetCartItemAsync(int id);
    
    Task<IResult<List<CartItemDto>>> GetCartAsync();
}