namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsCreator
{
    Task AddNewCartItemAsync(DAL.Models.Guitar guitar, string cartId);
}