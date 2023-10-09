using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Interfaces.CartItem;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain.Providers;

public class CartItemsProvider : ICartItemsProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IShopBackendService _shopBackendService;

    public CartItemsProvider(IShopBackendService shopBackendService,
        IHttpContextAccessor httpContextAccessor)
    {
        _shopBackendService = shopBackendService;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;
    private string CartString => Context.Session.GetString(Constants.Cart.Key);

    public async Task<IResultDto<CartItemDto>> GetCartItemAsync(int id)
    {
        return Context.User.Identity!.IsAuthenticated
            ? await GetDbCartItem(id)
            : GetSessionCartItem(id);
    }

    public async Task<IResultDto<List<CartItemDto>>> GetCartAsync()
    {
        if (Context.User.Identity is {IsAuthenticated: true})
        {
            return await _shopBackendService.GetAsync<List<CartItemDto>, string>(
                Constants.Routes.GetCart,Context.User.Identity.Name);
        }

        return ResultDtoCreator.GetValidResult(
            SessionCartProvider.GetCart(_httpContextAccessor));
    }

    private async Task<IResultDto<CartItemDto>> GetDbCartItem(int id)
    {
        var result = await GetCartAsync();
        if (result.Data == null)
        {
            return ResultDtoCreator.GetInvalidResult<CartItemDto>(
                Constants.Cart.CartEmpty);
        }

        foreach (var item in result.Data.Where(item => item.ProductId == id))
        {
            return ResultDtoCreator.GetValidResult(item);
        }

        return ResultDtoCreator.GetInvalidResult<CartItemDto>(
            Constants.Cart.ItemNotExist);
    }

    private IResultDto<CartItemDto> GetSessionCartItem(int id)
    {
        if (CartString == null)
        {
            return ResultDtoCreator.GetInvalidResult<CartItemDto>(
                Constants.Cart.ItemNotExist);
        }

        List<CartItemDto> cart = JsonConvert.DeserializeObject<List<CartItemDto>>(CartString);
        CartItemDto itemDto = cart?.FirstOrDefault(item => item.Product.Id == id);
        return itemDto == null
            ? ResultDtoCreator.GetInvalidResult<CartItemDto>(Constants.Cart.ItemNotExist)
            : ResultDtoCreator.GetValidResult(itemDto);
    }
}