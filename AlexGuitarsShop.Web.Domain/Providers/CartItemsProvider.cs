using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Interfaces.CartItem;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain.Providers;

public class CartItemsProvider : ICartItemsProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseMaker _responseMaker;

    public CartItemsProvider(IResponseMaker responseMaker,
        IHttpContextAccessor httpContextAccessor)
    {
        _responseMaker = responseMaker;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;
    private string CartString => Context.Session.GetString(Constants.Cart.Key);

    public async Task<IResult<CartItemDto>> GetCartItemAsync(int id)
    {
        return Context.User.Identity!.IsAuthenticated
            ? await GetDbCartItem(id)
            : GetSessionCartItem(id);
    }

    public async Task<IResult<List<CartItemDto>>> GetCartAsync()
    {
        if (Context.User.Identity is {IsAuthenticated: true})
        {
            return await _responseMaker.GetCartAsync(Context.User.Identity.Name);
        }

        return ResultCreator.GetValidResult(
            SessionCartProvider.GetCart(_httpContextAccessor), HttpStatusCode.OK);
    }

    private async Task<IResult<CartItemDto>> GetDbCartItem(int id)
    {
        var result = await GetCartAsync();
        if (result.Data == null)
        {
            return ResultCreator.GetInvalidResult<CartItemDto>(
                Constants.Cart.CartEmpty, HttpStatusCode.BadRequest);
        }

        foreach (var item in result.Data.Where(item => item.ProductId == id))
        {
            return ResultCreator.GetValidResult(item, HttpStatusCode.OK);
        }

        return ResultCreator.GetInvalidResult<CartItemDto>(
            Constants.Cart.ItemNotExist, HttpStatusCode.BadRequest);
    }

    private IResult<CartItemDto> GetSessionCartItem(int id)
    {
        if (CartString == null)
        {
            return ResultCreator.GetInvalidResult<CartItemDto>(
                Constants.Cart.ItemNotExist, HttpStatusCode.BadRequest);
        }

        List<CartItemDto> cart = JsonConvert.DeserializeObject<List<CartItemDto>>(CartString);
        CartItemDto itemDto = cart?.FirstOrDefault(item => item.Product.Id == id);
        return itemDto == null
            ? ResultCreator.GetInvalidResult<CartItemDto>(Constants.Cart.ItemNotExist, HttpStatusCode.BadRequest)
            : ResultCreator.GetValidResult(itemDto, HttpStatusCode.OK);
    }
}