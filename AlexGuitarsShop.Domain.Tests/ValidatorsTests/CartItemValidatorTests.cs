using System.Net;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Domain.Validators;
using Moq;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests.ValidatorsTests;

public class CartItemValidatorTests
{
    private Mock<ICartItemRepository> _cartItemRepositoryMock;
    private ICartItemValidator _cartItemValidator;

    [SetUp]
    public void Setup()
    {
        _cartItemRepositoryMock = new Mock<ICartItemRepository>();
        _cartItemValidator = new CartItemValidator(_cartItemRepositoryMock.Object);
    }

    [TestCase(10, 40)]
    public async Task CheckIfRegisterIsValid_InvalidInput_ReturnsInvalidResult(int id, int accountId)
    {
        // Arrange 
        var exceptedStatus = HttpStatusCode.BadRequest;
        var exceptedMessage = Constants.ErrorMessages.InvalidEmail;

        // Act
        var result = await _cartItemValidator.CheckIfCartItemExist(id, accountId);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(exceptedStatus, result.StatusCode);
        Assert.AreEqual(exceptedMessage, result.Error);
    }
}