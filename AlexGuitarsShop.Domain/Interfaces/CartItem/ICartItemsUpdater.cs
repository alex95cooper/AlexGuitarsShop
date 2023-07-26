namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsUpdater
{
    Task RemoveAsync(int id);

    Task IncrementAsync(int id);

    Task DecrementAsync(int id);

    Task OrderAsync();
}