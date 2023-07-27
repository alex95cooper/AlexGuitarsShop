namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsProvider
{
    Task<IResponse<DAL.Models.CartItem>> GetCartItemAsync(int id);
    Task<IResponse<List<DAL.Models.CartItem>>> GetCartItemsAsync();
}