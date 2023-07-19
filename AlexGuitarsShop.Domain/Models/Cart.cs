using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AlexGuitarsShop.Domain.Models;

public class Cart
{
    public Cart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
        Id = session.GetString("CartId") ?? Guid.NewGuid().ToString();
        session.SetString("CartId", Id);
        Products = new List<CartItem>();
    }

    public string Id { get; }
    
    public List<CartItem> Products { get; set; }
}