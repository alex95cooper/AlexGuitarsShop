namespace AlexGuitarsShop.Common.Models;

public class CartItemDto
{
    public int ProductId { get; init; }
    public int BuyerId { get; init; }
    public int Quantity { get; set; }
    public string BuyerEmail { get; init; }
    public GuitarDto Product { get; init; }
}