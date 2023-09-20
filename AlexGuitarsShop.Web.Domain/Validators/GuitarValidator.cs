using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain.Validators;

public class GuitarValidator : IGuitarValidator
{
    private readonly HttpClient _client;

    public GuitarValidator(HttpClient client)
    {
        _client = client;
    }

    public async Task<bool> CheckIfGuitarExist(int id)
    {
        using var response = await _client.GetAsync($"http://localhost:5001/Guitar/Update/{id}");
        var result =  JsonConvert.DeserializeObject<Result<Guitar>>(await response.Content
            .ReadAsStringAsync());
        return result!.IsSuccess;

    }
}