namespace AlexGuitarsShop.Common.Models;

public class CartItem
{
    public int ProductId{ get; init; }
    public int Quantity { get; set; }
    public Guitar Product { get; init; }
}