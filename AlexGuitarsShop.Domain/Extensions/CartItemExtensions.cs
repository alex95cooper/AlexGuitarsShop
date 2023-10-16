using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.Domain.Extensions;

public static class CartItemExtensions
{
    public static CartItemDto ToCartItemDto(this CartItem item)
    {
        return new CartItemDto
        {
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            Product = item.Product.ToGuitarDto()
        };
    }
}