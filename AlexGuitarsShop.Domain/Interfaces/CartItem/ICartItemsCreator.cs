namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsCreator
{
    Task AddNewCartItemAsync(Common.Models.Guitar guitar, int accountId);
}