using AlexGuitarsShop.Domain.Extensions;

namespace AlexGuitarsShop.Domain;

public static class ListMapper
{
    public static List<Common.Models.Account> ToDtoAccountList(IEnumerable<DAL.Models.Account> list)
    {
        return list.Select(account => new Common.Models.Account
        {
            Name = account.Name,
            Email = account.Email,
            Role = account.Role
        }).ToList();
    }
    
    public static List<Common.Models.CartItem> ToDtoCartItemList(IEnumerable<DAL.Models.CartItem> list)
    {
        return list.Select(cartItem => new Common.Models.CartItem
        {
            ProductId = cartItem.ProductId,
            Quantity = cartItem.Quantity,
            Product = cartItem.Product.ToGuitarDto()
        }).ToList();
    }
    
    public static List<Common.Models.Guitar> ToDtoGuitarList(IEnumerable<DAL.Models.Guitar> list)
    {
        return list.Select(guitar => new Common.Models.Guitar
        {
            Id = guitar.Id,
            Name = guitar.Name,
            Price = guitar.Price,
            Description = guitar.Description,
            Image = guitar.Image,
            IsDeleted = guitar.IsDeleted
        }).ToList();
    }
}