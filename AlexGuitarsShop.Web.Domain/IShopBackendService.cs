using AlexGuitarsShop.Common;

namespace AlexGuitarsShop.Web.Domain;

public interface IShopBackendService
{
    Task<IResultDto<TResult>> GetAsync<TResult, TData>(string route, TData parameter);
    
    Task<IResultDto<T>> PostAsync<T>(T modelDto, string route);

    Task<IResultDto<T>> PutAsync<T>(T modelDto, string route);

    Task<IResultDto<int>> DeleteAsync(string route);
}