using SixLabors.ImageSharp.Formats.Jpeg;

namespace AlexGuitarsShop.Scripts;

public static class ImageConverter
{
    public static async Task<string> GetBase64StringAsync(string path)
    {
        await using FileStream stream = File.OpenRead(path);
        Image image = await Image.LoadAsync(stream);
        await using var ms = new MemoryStream();
        await image.SaveAsync(ms, JpegFormat.Instance);
        byte[] bytes = ms.ToArray();
        return Convert.ToBase64String(bytes);
    }
}