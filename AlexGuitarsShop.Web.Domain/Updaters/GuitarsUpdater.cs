using System.Text;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Web.Domain.ViewModels;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain.Updaters;

public class GuitarsUpdater : IGuitarsUpdater
{
    private const string ErrorMessage = "The information about the account is not filled correctly!";
    
    private readonly HttpClient _client;

    public GuitarsUpdater(HttpClient client)
    {
        _client = client;
    }

    public async Task<IResult<Guitar>> UpdateGuitarAsync(GuitarViewModel model)
    {
        if (model == null)
        {
            return ResultCreator.GetInvalidResult<Guitar>(ErrorMessage);
        }
        
        Guitar guitar = model.ToGuitar();
        guitar.Image = model.Avatar == null ? model.Image : model.Avatar.ToBase64String();
        var objAsJson = JsonConvert.SerializeObject(guitar);
        var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");
        using var response = await _client.PostAsync("http://localhost:5001/Guitar/Update", content);
        return JsonConvert.DeserializeObject<Result<Guitar>>(await response.Content.ReadAsStringAsync());
    }

    public async Task<IResult<int>> DeleteGuitarAsync(int id)
    {
        using var response = await _client.GetAsync($"http://localhost:5001/Guitar/Delete/{id}");
        return JsonConvert.DeserializeObject<Result<int>>(await response.Content.ReadAsStringAsync());
    }
}