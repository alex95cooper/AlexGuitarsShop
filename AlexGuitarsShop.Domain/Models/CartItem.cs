namespace AlexGuitarsShop.Domain.Models;

public class CartItem
{
    public int Id { get; set; }
    public string CartId { get; set; }
    public int Quantity { get; set; }
    public Guitar Product { get; set; }
}