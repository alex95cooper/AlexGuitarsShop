using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Domain.Extensions;

namespace AlexGuitarsShop.Domain;

public static class ListMapper
{
    public static List<AccountDto> ToDtoAccountList(IEnumerable<DAL.Models.Account> list)
    {
        return list.Select(account => account.ToAccountDto()).ToList();
    }

    public static List<CartItemDto> ToDtoCartItemList(IEnumerable<DAL.Models.CartItem> list)
    {
        return list.Select(cartItem => cartItem.ToCartItemDto()).ToList();
    }

    public static List<GuitarDto> ToDtoGuitarList(IEnumerable<DAL.Models.Guitar> list)
    {
        return list.Select(guitar => guitar.ToGuitarDto()).ToList();
    }
}