namespace AlexGuitarsShop.DAL.Models;

public record Guitar(int Id, string Name,
    ushort Price, string Description, byte[] Image, ushort IsDeleted)
{
    public Guitar() : this(default, default, default,
        default, default, default) { }
}
