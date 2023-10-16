using AlexGuitarsShop.Common;

namespace AlexGuitarsShop.Web.Domain.Interfaces.CartItem;

public interface ICartItemsUpdater
{
    Task<IResultDto> RemoveAsync(int id);

    Task<IResultDto> IncrementAsync(int id);

    Task<IResultDto> DecrementAsync(int id);

    Task<IResultDto> OrderAsync();
}