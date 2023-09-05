using System.ComponentModel.DataAnnotations;

namespace AlexGuitarsShop.DAL.Models;

public class Guitar
{
    [Key]
    public int Id { get; init; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public ushort IsDeleted { get; set; }
    public virtual List<CartItem> CartItems { get; set; }
}
