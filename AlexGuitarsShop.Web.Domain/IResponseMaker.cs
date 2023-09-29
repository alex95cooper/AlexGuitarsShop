using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain;

public interface IResponseMaker
{
    Task<IResult<PaginatedListDto<T>>> GetListByLimitAsync<T>(string route, int pageNumber);

    Task<IResult<GuitarViewModel>> GetGuitarAsync(int id);

    Task<IResult<List<CartItemDto>>> GetCartAsync(string email);

    Task<IResult<T>> PostAsync<T>(T modelDto, string route);

    Task<IResult<T>> PutAsync<T>(T modelDto, string route);

    Task<IResult<int>> DeleteAsync(string route);
}