namespace AlexGuitarsShop.DAL.Models;

public class CartItem
{
    public int Quantity { get; init; }
    public Guitar Product { get; set; }
}

