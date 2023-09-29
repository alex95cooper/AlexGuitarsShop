using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsCreator
{
    Task<IResult<CartItemDto>> AddNewCartItemAsync(DAL.Models.Guitar guitar, int accountId);
}