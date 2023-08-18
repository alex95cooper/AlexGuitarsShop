namespace AlexGuitarsShop.Extensions;

public static class FormFileExtensions
{
    public static byte[] ToByteArray(this IFormFile avatar)
    {
        avatar = avatar ?? throw new ArgumentNullException(nameof(avatar));
        using var binaryReader = new BinaryReader(avatar.OpenReadStream());
        return binaryReader.ReadBytes((int) avatar.Length);

    }
}