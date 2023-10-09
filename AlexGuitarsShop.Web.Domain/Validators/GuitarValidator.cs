using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain.Validators;

public class GuitarValidator : IGuitarValidator
{
    private readonly HttpClient _client;
    private readonly BackendUrls _backendUrl;

    public GuitarValidator(IOptionsMonitor<BackendUrls> option, HttpClient client)
    {
        _backendUrl = option.CurrentValue;
        _client = client;
    }

    public async Task<bool> CheckIfGuitarExist(int id)
    {
        string path = Path.Combine(_backendUrl.DefaultUrl, string.Format(Constants.Routes.GetGuitar, id));
        using var response = await _client.GetAsync(path.Replace('\\', '/'));
        var result = JsonConvert.DeserializeObject<ResultDto<GuitarDto>>(await response.Content
            .ReadAsStringAsync());
        return result!.IsSuccess;
    }
}