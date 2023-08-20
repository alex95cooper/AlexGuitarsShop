namespace AlexGuitarsShop.ViewModels;

public class GuitarViewModel
{
    public int Id { get; init; }
    public string Name { get; init; }
    public ushort Price { get; init; }
    public string Description { get; init; }
    public IFormFile Avatar { get; init; }
    public byte[] Image { get; set; }
    public ushort IsDeleted { get; init; }
}