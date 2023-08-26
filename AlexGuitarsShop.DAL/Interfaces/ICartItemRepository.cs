using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.DAL.Interfaces;

public interface ICartItemRepository
{
    Task<CartItem> FindAsync(int id, string cartId);

    Task<int> GetProductQuantityAsync(int id, string cartId);

    Task<List<CartItem>> GetAllAsync(string cartId);

    Task CreateAsync(CartItem cartItem, string cartId);

    Task UpdateQuantityAsync(int id, int quantity, string cartId);

    Task DeleteAsync(int id, string cartId);

    Task DeleteAllAsync(string cartId);
}