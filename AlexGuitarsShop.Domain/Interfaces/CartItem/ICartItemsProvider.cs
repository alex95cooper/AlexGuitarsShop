using AlexGuitarsShop.Common;

namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsProvider
{
    Task<IResult<List<Common.Models.CartItem>>> GetCartAsync(int accountId);
}