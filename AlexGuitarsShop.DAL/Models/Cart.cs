using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AlexGuitarsShop.DAL.Models;

public class Cart
{
    public Cart(IServiceProvider services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        IHttpContextAccessor accessor = services.GetRequiredService<IHttpContextAccessor>();
        if (accessor == null) throw new ArgumentNullException(nameof(accessor));
        HttpContext context = accessor.HttpContext;
        if (context  == null) throw new ArgumentNullException(nameof(context));
        ISession session = context.Session;
        Id = session.GetString("CartId") ?? Guid.NewGuid().ToString();
        session.SetString("CartId", Id);
        Products = new List<CartItem>();
    }

    public string Id { get; }
    public List<CartItem> Products { get; set; }
}