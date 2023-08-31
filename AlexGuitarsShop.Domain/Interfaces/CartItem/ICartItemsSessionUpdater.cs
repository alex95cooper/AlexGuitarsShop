namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsSessionUpdater
{
    void Remove(int id, List<DAL.Models.CartItem> cart);

    void Increment(int id, List<DAL.Models.CartItem> cart);

    void Decrement(int id, List<DAL.Models.CartItem> cart);

    void Order();
}