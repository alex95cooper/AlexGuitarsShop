using System.Net;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.CartItem;

namespace AlexGuitarsShop.Domain.Providers;

public class CartItemsProvider : ICartItemsProvider
{
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemsProvider(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task<IResult<List<CartItemDto>>> GetCartAsync(int accountId)
    {
        var result = await _cartItemRepository.GetAllAsync(accountId);
        var listDto = ListMapper.ToDtoCartItemList(result);
        return result.Count == 0
            ? ResultCreator.GetValidResult(listDto, HttpStatusCode.NoContent)
            : ResultCreator.GetValidResult(listDto, HttpStatusCode.OK);
    }
}