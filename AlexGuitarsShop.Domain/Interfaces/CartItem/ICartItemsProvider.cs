namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsProvider
{
    Task<IResponse<List<DAL.Models.CartItem>>> GetCartItemsAsync();
}