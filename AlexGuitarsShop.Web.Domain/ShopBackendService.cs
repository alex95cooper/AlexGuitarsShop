using System.Text;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain;

public class ShopBackendService : IShopBackendService
{
    private readonly HttpClient _client;
    private readonly BackendUrls _backendUrl;

    public ShopBackendService(IOptionsMonitor<BackendUrls> option, HttpClient client)
    {
        _backendUrl = option.CurrentValue;
        _client = client;
    }

    public async Task<IResultDto<TResult>> GetAsync<TResult, TData>(string route, TData parameter)
    {
        try
        {
            string path = BuildPath(string.Format(route, parameter));
            using var response = await _client.GetAsync(path);
            return await Deserialize<TResult>(response);
        }
        catch
        {
            return BuildBadResult<TResult>();
        }
    }

    public async Task<IResultDto<AccountDto>> PostAsync(AccountDto modelDto, string route)
    {
        try
        {
            var content = BuildContent(modelDto);
            string path = BuildPath(route);
            using var response = await _client.PostAsync(path, content);
            return await Deserialize<AccountDto>(response);
        }
        catch
        {
            return BuildBadResult<AccountDto>();
        }
    }

    public async Task<IResultDto> PostAsync<T>(T modelDto, string route)
    {
        try
        {
            var content = BuildContent(modelDto);
            string path = BuildPath(route);
            using var response = await _client.PostAsync(path, content);
            return await Deserialize(response);
        }
        catch
        {
            return BuildBadResult<T>();
        }
    }

    public async Task<IResultDto> PutAsync<T>(T modelDto, string route)
    {
        try
        {
            var content = BuildContent(modelDto);
            string path = BuildPath(route);
            using var response = await _client.PutAsync(path, content);
            return await Deserialize(response);
        }
        catch
        {
            return BuildBadResult<T>();
        }
    }

    public async Task<IResultDto> DeleteAsync(string route)
    {
        try
        {
            string path = BuildPath(route);
            using var response = await _client.DeleteAsync(path);
            return await Deserialize(response);
        }
        catch
        {
            return BuildBadResult();
        }
    }

    private static StringContent BuildContent<T>(T modelDto)
    {
        string objAsJson = JsonConvert.SerializeObject(modelDto);
        return new StringContent(objAsJson, Encoding.UTF8, Constants.HttpClient.MediaType);
    }

    private string BuildPath(string route)
    {
        string path = Path.Combine(_backendUrl.DefaultUrl, route);
        return path.Replace('\\', '/');
    }

    private static async Task<ResultDto<T>> Deserialize<T>(HttpResponseMessage response)
    {
        return JsonConvert.DeserializeObject<ResultDto<T>>(await response.Content.ReadAsStringAsync());
    }

    private static async Task<ResultDto> Deserialize(HttpResponseMessage response)
    {
        return JsonConvert.DeserializeObject<ResultDto>(await response.Content.ReadAsStringAsync());
    }

    private static ResultDto<T> BuildBadResult<T>()
    {
        return ResultDtoCreator.GetInvalidResult<T>(
            Constants.ErrorMessages.ServerError);
    }

    private static ResultDto BuildBadResult()
    {
        return ResultDtoCreator.GetInvalidResult(
            Constants.ErrorMessages.ServerError);
    }
}