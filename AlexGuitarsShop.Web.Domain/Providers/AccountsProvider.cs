using System.Text;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Account;
using AlexGuitarsShop.Web.Domain.ViewModels;
using Newtonsoft.Json;
using AccountDto = AlexGuitarsShop.Common.Models.Account;

namespace AlexGuitarsShop.Web.Domain.Providers;

public class AccountsProvider : IAccountsProvider
{
    private readonly HttpClient _client;

    public AccountsProvider(HttpClient client)
    {
        _client = client;
    }

    public async Task<IResult<AccountDto>> GetAccountAsync(LoginViewModel model)
    {
        Login login = model.ToLogin() ?? throw new ArgumentNullException(nameof(model));
        var objAsJson = JsonConvert.SerializeObject(login);
        var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");
        using var response = await _client.PostAsync("http://localhost:5001/Account/Login", content);
        return JsonConvert.DeserializeObject<Result<AccountDto>>(await response.Content.ReadAsStringAsync());
    }

    public async Task<IResult<PaginatedListViewModel<AccountDto>>> GetAdminsAsync(int pageNumber)
    {
        using var response = await _client.GetAsync($"http://localhost:5001/Account/Admins/{pageNumber}");
        var result = JsonConvert.DeserializeObject<Result<PaginatedList<AccountDto>>>(await response.Content
            .ReadAsStringAsync());
        return result is {IsSuccess: true}
            ? ResultCreator.GetValidResult(result.Data.ToPaginatedListViewModel(Title.Admins, pageNumber))
            : ResultCreator.GetInvalidResult<PaginatedListViewModel<AccountDto>>(result!.Error);
    }

    public async Task<IResult<PaginatedListViewModel<AccountDto>>> GetUsersAsync(int pageNumber)
    {
        using var response = await _client.GetAsync($"http://localhost:5001/Account/Users/{pageNumber}");
        var result = JsonConvert.DeserializeObject<Result<PaginatedList<AccountDto>>>(await response.Content
            .ReadAsStringAsync());
        return result is {IsSuccess: true}
            ? ResultCreator.GetValidResult(result.Data.ToPaginatedListViewModel(Title.Users, pageNumber))
            : ResultCreator.GetInvalidResult<PaginatedListViewModel<AccountDto>>(result!.Error);
    }
}