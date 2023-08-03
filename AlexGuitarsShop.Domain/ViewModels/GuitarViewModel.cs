using Microsoft.AspNetCore.Http;

namespace AlexGuitarsShop.Domain.ViewModels;

public record GuitarViewModel(int Id, string Name, ushort Price, 
    string Description, IFormFile Avatar, byte[] Image, ushort IsDeleted)
{
    public GuitarViewModel() : this(default, default, default,
        default, default, default, default) { }
    
    public byte[] Image { get; set; } = Image;
}