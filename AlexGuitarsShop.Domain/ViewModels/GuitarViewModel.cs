using Microsoft.AspNetCore.Http;

namespace AlexGuitarsShop.Domain.ViewModels;

public class GuitarViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ushort Price { get; set; }
    public string Description { get; set; }
    public IFormFile Avatar { get; set; }
    public byte[]? Image { get; set; }
    public ushort IsDeleted { get; set; }
}