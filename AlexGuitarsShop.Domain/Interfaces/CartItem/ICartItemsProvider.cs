namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsProvider
{
    Task<IResult<DAL.Models.CartItem>> GetCartItemAsync(int id);
    Task<IResult<List<DAL.Models.CartItem>>> GetCartItemsAsync();
}