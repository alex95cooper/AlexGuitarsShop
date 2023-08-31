namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsSessionProvider
{
    List<DAL.Models.CartItem> GetCart();

    DAL.Models.CartItem GetCartItem(int id);
}