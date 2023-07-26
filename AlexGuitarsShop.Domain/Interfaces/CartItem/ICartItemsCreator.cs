namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsCreator
{
    Task AddCartItemAsync(DAL.Models.CartItem item);
}