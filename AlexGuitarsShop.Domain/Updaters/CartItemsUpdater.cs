using System.Net;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.CartItem;

namespace AlexGuitarsShop.Domain.Updaters;

public class CartItemsUpdater : ICartItemsUpdater
{
    private const int MinQuantity = 1;
    private const int MaxQuantity = 10;

    private readonly ICartItemRepository _cartItemRepository;

    public CartItemsUpdater(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task<IResult<int>> RemoveAsync(int id, int accountId)
    {
        await _cartItemRepository.DeleteAsync(id, accountId);
        return ResultCreator.GetValidResult(id, HttpStatusCode.OK);
    }

    public async Task<IResult<CartItemDto>> IncrementAsync(int id, int accountId)
    {
        int quantity = await _cartItemRepository.GetProductQuantityAsync(id, accountId) + 1;
        if (quantity <= MaxQuantity)
        {
            await _cartItemRepository.UpdateQuantityAsync(id, accountId, quantity);
        }

        return ResultCreator.GetValidResult(new CartItemDto {ProductId = id}, HttpStatusCode.OK);
    }

    public async Task<IResult<CartItemDto>> DecrementAsync(int id, int accountId)
    {
        int quantity = await _cartItemRepository.GetProductQuantityAsync(id, accountId) - 1;
        if (quantity >= MinQuantity)
        {
            await _cartItemRepository.UpdateQuantityAsync(id, accountId, quantity);
        }

        return ResultCreator.GetValidResult(new CartItemDto {ProductId = id}, HttpStatusCode.OK);
    }

    public async Task<IResult<string>> OrderAsync(int accountId)
    {
        await _cartItemRepository.DeleteAllAsync(accountId);
        return ResultCreator.GetValidResult("", HttpStatusCode.OK);
    }
}