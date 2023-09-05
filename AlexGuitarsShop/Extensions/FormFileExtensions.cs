namespace AlexGuitarsShop.Extensions;

public static class FormFileExtensions
{
    public static string ToBase64String(this IFormFile avatar)
    {
        using var binaryReader = new BinaryReader(avatar.OpenReadStream());
        byte[] bytes = binaryReader.ReadBytes((int) avatar.Length);
        return  Convert.ToBase64String(bytes);
    }
}