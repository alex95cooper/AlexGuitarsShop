using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.CartItem;

public interface ICartItemsCreator
{
    Task<IResultDto<CartItemDto>> AddNewCartItemAsync(GuitarViewModel model);
}