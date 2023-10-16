using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsUpdater
{
    Task<IResult> RemoveAsync(int id, int accountId);

    Task<IResult> IncrementAsync(int id, int accountId);

    Task<IResult> DecrementAsync(int id, int accountId);

    Task<IResult> OrderAsync(int accountId);
}