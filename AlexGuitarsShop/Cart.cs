using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop;

public class Cart
{
    public string Id { get; private init; }
    public List<CartItem> Products { get; set; }

    public static Cart GetCart(IHttpContextAccessor accessor)
    {
        ISession session = accessor.HttpContext?.Session;
        string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
        session?.SetString("CartId", cartId);
        return new Cart {Id = cartId, Products = new List<CartItem>()};
    }
}