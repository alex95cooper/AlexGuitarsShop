namespace AlexGuitarsShop.DAL.Models;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; init; }
    public Guitar Product { get; set; }
}