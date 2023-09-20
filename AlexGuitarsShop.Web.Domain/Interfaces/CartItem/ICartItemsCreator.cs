using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.CartItem;

public interface ICartItemsCreator
{
    Task AddNewCartItemAsync(GuitarViewModel model);
}