using System.Net;
using System.Text;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.ViewModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain;

public class ResponseMaker : IResponseMaker
{
    private readonly HttpClient _client;
    private readonly BackendUrls _backendUrl;

    public ResponseMaker(IOptionsMonitor<BackendUrls> option, HttpClient client)
    {
        _backendUrl = option.CurrentValue;
        _client = client;
    }

    public async Task<IResult<PaginatedListDto<T>>> GetListByLimitAsync<T>(string route, int pageNumber)
    {
        try
        {
            using var response = await GetAsync(string.Format(route, pageNumber));
            return JsonConvert.DeserializeObject<Result<PaginatedListDto<T>>>(
                await response.Content.ReadAsStringAsync());
        }
        catch
        {
            return ResultCreator.GetInvalidResult<PaginatedListDto<T>>(
                Constants.ErrorMessages.ServerError, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<IResult<GuitarViewModel>> GetGuitarAsync(int id)
    {
        try
        {
            using var response = await GetAsync(string.Format(Constants.Routes.GetGuitar, id));
            var result = JsonConvert.DeserializeObject<Result<GuitarDto>>(await response.Content.ReadAsStringAsync());
            return result is {IsSuccess: true}
                ? ResultCreator.GetValidResult(result.Data.ToGuitarViewModel(), result.StatusCode)
                : ResultCreator.GetInvalidResult<GuitarViewModel>(result!.Error, result.StatusCode);
        }
        catch
        {
            return ResultCreator.GetInvalidResult<GuitarViewModel>(
                Constants.ErrorMessages.ServerError, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<IResult<List<CartItemDto>>> GetCartAsync(string email)
    {
        try
        {
            using var response = await GetAsync(string.Format(Constants.Routes.GetCart, email));
            return JsonConvert.DeserializeObject<Result<List<CartItemDto>>>(
                await response.Content.ReadAsStringAsync());
        }
        catch
        {
            return ResultCreator.GetInvalidResult<List<CartItemDto>>(
                Constants.ErrorMessages.ServerError, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<IResult<T>> PostAsync<T>(T modelDto, string route)
    {
        try
        {
            var content = GetContent(modelDto);
            string path = Path.Combine(_backendUrl.DefaultUrl, route);
            using var response = await _client.PostAsync(path.Replace('\\', '/'), content);
            return JsonConvert.DeserializeObject<Result<T>>(await response.Content.ReadAsStringAsync());
        }
        catch
        {
            return ResultCreator.GetInvalidResult<T>(
                Constants.ErrorMessages.ServerError, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<IResult<T>> PutAsync<T>(T modelDto, string route)
    {
        try
        {
            var content = GetContent(modelDto);
            string path = Path.Combine(_backendUrl.DefaultUrl, route);
            using var response = await _client.PutAsync(path.Replace('\\', '/'), content);
            return JsonConvert.DeserializeObject<Result<T>>(await response.Content.ReadAsStringAsync());
        }
        catch
        {
            return ResultCreator.GetInvalidResult<T>(
                Constants.ErrorMessages.ServerError, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<IResult<int>> DeleteAsync(string route)
    {
        try
        {
            string path = Path.Combine(_backendUrl.DefaultUrl, route);
            using var response = await _client.DeleteAsync(path.Replace('\\', '/'));
            return JsonConvert.DeserializeObject<Result<int>>(await response.Content.ReadAsStringAsync());
        }
        catch
        {
            return ResultCreator.GetInvalidResult<int>(
                Constants.ErrorMessages.ServerError, HttpStatusCode.InternalServerError);
        }
    }

    private async Task<HttpResponseMessage> GetAsync(string route)
    {
        string path = Path.Combine(_backendUrl.DefaultUrl, string.Format(route));
        return await _client.GetAsync(path.Replace('\\', '/'));
    }

    private static StringContent GetContent<T>(T modelDto)
    {
        string objAsJson = JsonConvert.SerializeObject(modelDto);
        return new StringContent(objAsJson, Encoding.UTF8, Constants.HttpClient.MediaType);
    }
}