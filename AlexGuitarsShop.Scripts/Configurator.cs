using System.Text.Json;

namespace AlexGuitarsShop.Scripts;

public class Configurator : IConfigurator
{
    public string GetConnectionString()
    {
        string json = File.ReadAllText("appsettings.json");
        JsonDocument appSettings = JsonDocument.Parse(
            json, new JsonDocumentOptions {CommentHandling = JsonCommentHandling.Skip});
        return appSettings.RootElement.GetProperty("ConnectionStrings").GetProperty("DefaultConnection").GetString();
    }
}