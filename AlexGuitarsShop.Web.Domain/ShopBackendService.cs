using System.Text;
using AlexGuitarsShop.Common;
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
            string path = GetPath(string.Format(route, parameter));
            using var response = await _client.GetAsync(path);
            return await Deserializable<TResult>(response);
        }
        catch
        {
            return GetBadResult<TResult>();
        }
    }

    public async Task<IResultDto<T>> PostAsync<T>(T modelDto, string route)
    {
        try
        {
            var content = GetContent(modelDto);
            string path = GetPath(route);
            using var response = await _client.PostAsync(path, content);
            return await Deserializable<T>(response);
        }
        catch
        {
            return GetBadResult<T>();
        }
    }

    public async Task<IResultDto<T>> PutAsync<T>(T modelDto, string route)
    {
        try
        {
            var content = GetContent(modelDto);
            string path = GetPath(route);
            using var response = await _client.PutAsync(path, content);
            return await Deserializable<T>(response);
        }
        catch
        {
            return GetBadResult<T>();
        }
    }

    public async Task<IResultDto<int>> DeleteAsync(string route)
    {
        try
        {
            string path = GetPath(route);
            using var response = await _client.DeleteAsync(path);
            return await Deserializable<int>(response);
        }
        catch
        {
            return GetBadResult<int>();
        }
    }

    private static StringContent GetContent<T>(T modelDto)
    {
        string objAsJson = JsonConvert.SerializeObject(modelDto);
        return new StringContent(objAsJson, Encoding.UTF8, Constants.HttpClient.MediaType);
    }

    private string GetPath(string route)
    {
        string path = Path.Combine(_backendUrl.DefaultUrl, route);
        return path.Replace('\\', '/');
    }

    private async Task<ResultDto<T>> Deserializable<T>(HttpResponseMessage response)
    {
        return JsonConvert.DeserializeObject<ResultDto<T>>(await response.Content.ReadAsStringAsync());
    }

    private ResultDto<T> GetBadResult<T>()
    {
        return ResultDtoCreator.GetInvalidResult<T>(
            Constants.ErrorMessages.ServerError);
    }
}