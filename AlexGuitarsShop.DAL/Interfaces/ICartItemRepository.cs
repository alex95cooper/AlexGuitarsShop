using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.DAL.Interfaces;

public interface ICartItemRepository
{
    Task<CartItem> FindAsync(int id);

    Task<int> GetProductQuantityAsync(int id);

    Task<List<CartItem>> GetAllAsync();

    Task CreateAsync(CartItem cartItem);

    Task UpdateQuantityAsync(int id, int quantity);

    Task DeleteAsync(int id);

    Task DeleteAllAsync();
}