namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsProvider
{
    Task<IResult<DAL.Models.CartItem>> GetCartItemAsync(int id, string cartId);
    
    Task<IResult<List<DAL.Models.CartItem>>> GetCartItemsAsync(string cartId);
}