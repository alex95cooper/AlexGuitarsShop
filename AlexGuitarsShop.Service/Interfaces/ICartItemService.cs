using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Models;

namespace AlexGuitarsShop.Service.Interfaces;

public interface ICartItemService
{
    Task<IResponse<List<CartItem>>> GetCartItems();
    
    Task Remove (int id);
    
    Task Increment(int id);
    
    Task Decrement(int id);

    Task Order();
}