namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemValidator
{
    Task<bool> CheckIfCartItemExist(int id, string cartId);
}