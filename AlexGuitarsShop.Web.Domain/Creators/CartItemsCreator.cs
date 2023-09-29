using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Web.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain.Creators;

public class CartItemsCreator : ICartItemsCreator
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseMaker _responseMaker;

    public CartItemsCreator(IResponseMaker responseMaker,
        IHttpContextAccessor httpContextAccessor)
    {
        _responseMaker = responseMaker;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;

    private string CartString
    {
        set => Context.Session.SetString(Constants.Cart.Key, value);
    }

    public async Task<IResult<CartItemDto>> AddNewCartItemAsync(GuitarViewModel model)
    {
        GuitarDto guitarDto = model.ToGuitar();
        if (Context.User.Identity is {IsAuthenticated: true})
        {
            return await AddToDbAsync(guitarDto.Id);
        }

        return AddToSession(guitarDto);
    }

    private IResult<CartItemDto> AddToSession(GuitarDto guitarDto)
    {
        List<CartItemDto> cart = SessionCartProvider.GetCart(_httpContextAccessor);
        CartItemDto itemDto = new() {Quantity = 1, Product = guitarDto};
        cart.Add(itemDto);
        CartString = JsonConvert.SerializeObject(cart);
        return ResultCreator.GetValidResult(itemDto, HttpStatusCode.OK);
    }

    private async Task<IResult<CartItemDto>> AddToDbAsync(int id)
    {
        CartItemDto item = new() {ProductId = id, BuyerEmail = Context.User.Identity!.Name, Quantity = 1};
        var result = await _responseMaker.PostAsync(item, Constants.Routes.AddCartItem);
        return result;
    }
}