using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Web.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain.Creators;

public class CartItemsCreator : ICartItemsCreator
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartItemsCreator(HttpClient client, IHttpContextAccessor httpContextAccessor)
    {
        _client = client;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;

    private string CartString
    {
        set => Context.Session.SetString(Constants.Cart.Key, value);
    }

    public async Task AddNewCartItemAsync(GuitarViewModel model)
    {
        Guitar guitar = model.ToGuitar() ?? throw new ArgumentNullException(nameof(model));
        if (Context.User.Identity is {IsAuthenticated: true})
        {
            await AddToDbAsync(guitar.Id);
        }
        else
        {
            AddToSession(guitar);
        }
    }

    private void AddToSession(Guitar guitar)
    {
        List<CartItem> cart = SessionCartProvider.GetCart(_httpContextAccessor);
        CartItem item = new() {Quantity = 1, Product = guitar};
        cart.Add(item);
        CartString = JsonConvert.SerializeObject(cart);
    }

    private async Task AddToDbAsync(int id)
    {
         await _client.GetAsync(
             $"http://localhost:5001/Cart/Add/id={id}&email={Context.User.Identity!.Name}");
    }
}