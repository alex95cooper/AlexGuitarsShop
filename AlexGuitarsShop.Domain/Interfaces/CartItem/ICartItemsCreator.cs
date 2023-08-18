using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsCreator
{
    Task AddNewCartItemAsync(GuitarViewModel model);
    Task AddCartItemAsync(DAL.Models.CartItem item);
}