using AlexGuitarsShop.Common.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AlexGuitarsShop.Web.Domain;

internal static class SessionCartProvider
{
    internal static List<CartItem> GetCart(IHttpContextAccessor httpContextAccessor)
    {
        HttpContext context = httpContextAccessor.HttpContext;
        string cartString = context.Session.GetString(Constants.Cart.Key);
        return cartString == null
            ? new List<CartItem>()
            : JsonConvert.DeserializeObject<List<CartItem>>(cartString);
    }
}