using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Interfaces.CartItem;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain.Providers;

public class CartItemsProvider : ICartItemsProvider
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartItemsProvider(IHttpContextAccessor httpContextAccessor, HttpClient client)
    {
        _client = client;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;
    private string CartString => Context.Session.GetString(Constants.Cart.Key);

    public async Task<IResult<CartItem>> GetCartItemAsync(int id)
    {
        return Context.User.Identity!.IsAuthenticated
            ? await GetDbCartItem(id)
            : GetSessionCartItem(id);
    }

    public async Task<IResult<List<CartItem>>> GetCartAsync()
    {
        if (Context.User.Identity is {IsAuthenticated: true})
        {
            using var response = await _client.GetAsync(
                $"http://localhost:5001/Cart/Index/{Context.User.Identity.Name}");
            return JsonConvert.DeserializeObject<Result<List<CartItem>>>(await response.Content
                .ReadAsStringAsync());
        }

        return ResultCreator.GetValidResult(SessionCartProvider.GetCart(_httpContextAccessor));
    }

    private async Task<IResult<CartItem>> GetDbCartItem(int id)
    {
        using var response = await _client.GetAsync(
            $"http://localhost:5001/Cart/Index/{Context.User.Identity!.Name}");
        var result = JsonConvert.DeserializeObject<Result<List<CartItem>>>(await response.Content
            .ReadAsStringAsync());
        if (result!.Data == null)
        {
            return ResultCreator.GetInvalidResult<CartItem>(Constants.Cart.CartEmpty);
        }

        foreach (var item in result.Data.Where(item => item.ProductId == id))
        {
            return ResultCreator.GetValidResult(item);
        }

        return ResultCreator.GetInvalidResult<CartItem>(Constants.Cart.ItemNotExist);
    }

    private IResult<CartItem> GetSessionCartItem(int id)
    {
        if (CartString == null)
        {
            return ResultCreator.GetInvalidResult<CartItem>(Constants.Cart.ItemNotExist);
        }

        List<CartItem> cart = JsonConvert.DeserializeObject<List<CartItem>>(CartString);
        CartItem item = cart?.FirstOrDefault(item => item.Product.Id == id);
        return item == null
            ? ResultCreator.GetInvalidResult<CartItem>(Constants.Cart.ItemNotExist)
            : ResultCreator.GetValidResult(item);
    }
}