using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Web.Domain.ViewModels;
using Newtonsoft.Json;
using GuitarDto = AlexGuitarsShop.Common.Models.Guitar;

namespace AlexGuitarsShop.Web.Domain.Providers;

public class GuitarsProvider : IGuitarsProvider
{
    private readonly HttpClient _client;

    public GuitarsProvider(HttpClient client)
    {
        _client = client;
    }

    public async Task<IResult<PaginatedListViewModel<GuitarDto>>> GetGuitarsByLimitAsync(int pageNumber)
    {
        using var response = await _client.GetAsync($"http://localhost:5001/Guitar/Index/{pageNumber}");
        var result = JsonConvert.DeserializeObject<Result<PaginatedList<GuitarDto>>>(await response.Content
            .ReadAsStringAsync());
        return result is {IsSuccess: true}
            ? ResultCreator.GetValidResult(result.Data.ToPaginatedListViewModel(Title.Catalog, pageNumber))
            : ResultCreator.GetInvalidResult<PaginatedListViewModel<GuitarDto>>(result!.Error);
    }

    public async Task<IResult<GuitarViewModel>> GetGuitarAsync(int id)
    {
        using var response = await _client.GetAsync($"http://localhost:5001/Guitar/Update/{id}");
        var result = JsonConvert.DeserializeObject<Result<GuitarDto>>(await response.Content
            .ReadAsStringAsync());
        return result is {IsSuccess: true}
            ? ResultCreator.GetValidResult(result.Data.ToGuitarViewModel())
            : ResultCreator.GetInvalidResult<GuitarViewModel>(result!.Error);
    }
}