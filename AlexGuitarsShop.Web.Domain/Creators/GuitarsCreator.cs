using System.Text;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Web.Domain.ViewModels;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain.Creators;

public class GuitarsCreator : IGuitarsCreator
{
    private readonly HttpClient _client;

    public GuitarsCreator(HttpClient client)
    {
        _client = client;
    }

    public async Task<IResult<Guitar>> AddGuitarAsync(GuitarViewModel model)
    {
        if (model == null)
        {
            return ResultCreator.GetInvalidResult<Guitar>(Constants.Guitar.IncorrectGuitar);
        }
        
        Guitar guitar = model.ToGuitar();
        guitar.Image = model!.Avatar == null ? model.Image : model.Avatar.ToBase64String();
        var objAsJson = JsonConvert.SerializeObject(guitar);
        var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");
        using var response = await _client.PostAsync("http://localhost:5001/Guitar/Add", content);
        return JsonConvert.DeserializeObject<Result<Guitar>>(await response.Content.ReadAsStringAsync());
    }
}