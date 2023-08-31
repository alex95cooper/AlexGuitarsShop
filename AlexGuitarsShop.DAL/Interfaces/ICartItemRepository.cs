using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.DAL.Interfaces;

public interface ICartItemRepository
{
    Task<CartItem> FindAsync(int id, int accountId);

    Task<int> GetProductQuantityAsync(int id, int accountId);

    Task<List<CartItem>> GetAllAsync(int accountId);

    Task CreateAsync(CartItem cartItem, int accountId);

    Task UpdateQuantityAsync(int id, int accountId, int quantity);

    Task DeleteAsync(int id, int accountId);

    Task DeleteAllAsync(int accountId);
}