namespace AlexGuitarsShop.ViewModels;

public class GuitarViewModel
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int Price { get; init; }
    public string Description { get; init; }
    public IFormFile Avatar { get; init; }
    public string Image { get; init; }
    public ushort IsDeleted { get; init; }
}