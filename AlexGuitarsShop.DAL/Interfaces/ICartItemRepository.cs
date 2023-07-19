using AlexGuitarsShop.Domain.Models;

namespace AlexGuitarsShop.DAL.Interfaces;

public interface ICartItemRepository: IRepository<CartItem>
{
    Task<int> GetProductQuantity(int id);

    Task ChangeQuantity(int id, int quantity);
    
    Task DeleteAll();
}