using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.DAL.Interfaces;

public interface ICartItemRepository: IRepository<CartItem>
{
    Task<int> GetProductQuantityAsync(int id);

    Task ChangeQuantityAsync(int id, int quantity);
    
    Task DeleteAllAsync();
}