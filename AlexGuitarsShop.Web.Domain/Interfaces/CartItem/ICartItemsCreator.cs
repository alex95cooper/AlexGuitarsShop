using AlexGuitarsShop.Common;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.CartItem;

public interface ICartItemsCreator
{
    Task<IResultDto> AddNewCartItemAsync(GuitarViewModel model);
}