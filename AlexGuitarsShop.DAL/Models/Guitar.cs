namespace AlexGuitarsShop.DAL.Models;

public class Guitar
{
    public int Id { get; init; }
    public string Name { get; init; }
    public ushort Price { get; init; }
    public string Description { get; init; }
    public byte[] Image { get; init; }
    public ushort IsDeleted { get; init; }
}