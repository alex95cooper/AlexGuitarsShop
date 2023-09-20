using AlexGuitarsShop.Common;
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

    public async Task<IResult<List<Common.Models.CartItem>>> GetCartAsync(int accountId)
    {
        var result = await _cartItemRepository.GetAllAsync(accountId);
        return ResultCreator.GetValidResult(ListMapper.ToDtoCartItemList(result));
    }
}