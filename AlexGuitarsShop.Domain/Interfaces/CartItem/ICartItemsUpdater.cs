namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsUpdater
{
    Task RemoveAsync(int id, int accountId);

    Task IncrementAsync(int id, int accountId);

    Task DecrementAsync(int id, int accountId);

    Task OrderAsync(int accountId);
}