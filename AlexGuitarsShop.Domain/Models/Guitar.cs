namespace AlexGuitarsShop.Domain.Models;

public class Guitar
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ushort Price { get; set; }
    public string Description { get; set; }
    public byte[]? Image { get; set; }
    public ushort IsDeleted { get; set; }
}