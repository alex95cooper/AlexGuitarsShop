using System.Net;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.CartItem;

namespace AlexGuitarsShop.Domain.Validators;

public class CartItemValidator : ICartItemValidator
{
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemValidator(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task<IResult<CartItemDto>> CheckIfCartItemExist(int id, int accountId)
    {
        return await _cartItemRepository.FindAsync(id, accountId) != null
            ? ResultCreator.GetValidResult(
                new CartItemDto {ProductId = id, BuyerId = accountId}, HttpStatusCode.OK)
            : ResultCreator.GetInvalidResult<CartItemDto>(
                Constants.ErrorMessages.InvalidEmail, HttpStatusCode.BadRequest);
    }
}