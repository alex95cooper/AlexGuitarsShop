using SixLabors.ImageSharp.Formats.Jpeg;

namespace AlexGuitarsShop.Scripts;

public static class ImageConverter
{
    public static string GetBase64String(string path)
    {
        using FileStream stream = File.OpenRead(path);
        Image image = Image.Load(stream);
        using var ms = new MemoryStream();
        image.Save(ms, JpegFormat.Instance);
        byte[] bytes = ms.ToArray();
        return  Convert.ToBase64String(bytes);
    }
}