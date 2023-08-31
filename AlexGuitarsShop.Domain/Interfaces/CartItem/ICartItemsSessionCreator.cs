namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsSessionCreator
{
    void AddCartItem(DAL.Models.Guitar guitar, List<DAL.Models.CartItem> cart);
}