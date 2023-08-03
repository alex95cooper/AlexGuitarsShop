namespace AlexGuitarsShop.DAL.Models;

public record CartItem(int Quantity, Guitar Product)
{
    public CartItem() : this(default, default){ }
    
    public Guitar Product { get; set; } = Product;
}

