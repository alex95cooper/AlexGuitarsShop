using AlexGuitarsShop.Common;
using AlexGuitarsShop.Web.Domain.Interfaces.Account;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain.Updaters;

public class AccountsUpdater : IAccountsUpdater
{
    private readonly HttpClient _client;

    public AccountsUpdater(HttpClient client)
    {
        _client = client;
    }

    public async Task<IResult<string>> SetAdminRightsAsync(string email)
    {
        using var response = await _client.GetAsync($"http://localhost:5001/Account/MakeAdmin/{email}");
        return JsonConvert.DeserializeObject<Result<string>>(await response.Content
            .ReadAsStringAsync());
    }

    public async Task<IResult<string>> RemoveAdminRightsAsync(string email)
    {
        using var response = await _client.GetAsync($"http://localhost:5001/Account/MakeUser/{email}");
        return JsonConvert.DeserializeObject<Result<string>>(await response.Content
            .ReadAsStringAsync());
    }
}