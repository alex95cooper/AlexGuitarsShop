namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsUpdater
{
    Task RemoveAsync(int id, string cartId);

    Task IncrementAsync(int id, string cartId);

    Task DecrementAsync(int id, string cartId);

    Task OrderAsync(string cartId);
}