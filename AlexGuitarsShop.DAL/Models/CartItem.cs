using System.ComponentModel.DataAnnotations;

namespace AlexGuitarsShop.DAL.Models;

public class CartItem
{
    [Key]
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int ProductId{ get; set; }
    public int Quantity { get; set; }
    public Account Account{ get; set; }
    public Guitar Product { get; set; }
    
    
}

