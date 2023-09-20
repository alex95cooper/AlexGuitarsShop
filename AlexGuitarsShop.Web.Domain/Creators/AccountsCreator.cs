using System.Text;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Account;
using AlexGuitarsShop.Web.Domain.ViewModels;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain.Creators;

public class AccountsCreator : IAccountsCreator
{
    private readonly HttpClient _client;

    public AccountsCreator(HttpClient client)
    {
        _client = client;
    }

    public async Task<IResult<Account>> AddAccountAsync(RegisterViewModel model)
    {
        Register register = model.ToRegister() ?? throw new ArgumentNullException(nameof(model));
        var objAsJson = JsonConvert.SerializeObject(register);
        var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");
        using var response = await _client.PostAsync("http://localhost:5001/Account/Register", content);
        return JsonConvert.DeserializeObject<Result<Account>>( await response.Content.ReadAsStringAsync());
    }
}