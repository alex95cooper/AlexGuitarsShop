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

    [Test]
    public async Task CheckIfRegisterIsValid_ValidInput_ReturnsInvalidResult()
    {
        // Arrange 
        int id = 10;
        int accountId = 40;
        var exceptedStatusCode = HttpStatusCode.BadRequest;
        var exceptedMessage = Constants.ErrorMessages.InvalidEmail;

        // Act
        var result = await _cartItemValidator.CheckIfCartItemExist(id, accountId);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(exceptedStatusCode, result.StatusCode);
        Assert.AreEqual(exceptedMessage, result.Error);
    }
}