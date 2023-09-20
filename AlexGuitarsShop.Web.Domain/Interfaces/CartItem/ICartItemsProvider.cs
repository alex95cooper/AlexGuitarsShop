using AlexGuitarsShop.Common;

namespace AlexGuitarsShop.Web.Domain.Interfaces.CartItem;

public interface ICartItemsProvider
{
    Task<IResult<Common.Models.CartItem>> GetCartItemAsync(int id);
    
    Task<IResult<List<Common.Models.CartItem>>> GetCartAsync();
}