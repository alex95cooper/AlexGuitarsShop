namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsProvider
{
    Task<IResult<DAL.Models.CartItem>> GetCartItemAsync(int id, int accountId);
    
    Task<IResult<List<DAL.Models.CartItem>>> GetCartItemsAsync(int accountId);
}