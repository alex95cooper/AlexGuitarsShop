using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Web.Domain;

public interface IShopBackendService
{
    Task<IResultDto<TResult>> GetAsync<TResult, TData>(string route, TData parameter);

    Task<IResultDto<AccountDto>> PostAsync(AccountDto modelDto, string route);

    Task<IResultDto> PostAsync<T>(T modelDto, string route);

    Task<IResultDto> PutAsync<T>(T modelDto, string route);

    Task<IResultDto> DeleteAsync(string route);
}