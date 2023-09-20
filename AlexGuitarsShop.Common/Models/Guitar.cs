namespace AlexGuitarsShop.Common.Models;

public class Guitar
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int Price { get; init; }
    public string Description { get; init; }
    public string Image { get; set; }
    public ushort IsDeleted { get; init; }
}