using System.Net;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Creators;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using Moq;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests;

public class CartItemsCreatorTests
{
    private Mock<ICartItemRepository> _cartItemRepositoryMock;
    private ICartItemsCreator _cartItemsCreator;

    [SetUp]
    public void Setup()
    {
        _cartItemRepositoryMock = new Mock<ICartItemRepository>();
        _cartItemsCreator = new CartItemsCreator(_cartItemRepositoryMock.Object);
    }

    [Test]
    public async Task AddNewCartItemAsync_ValidInput_ReturnsCorrectResult()
    {
        // Arrange
        var guitar = new Guitar {Id = 35, Name = "Super guitar"};
        int accountId = 1;
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var result = await _cartItemsCreator.AddNewCartItemAsync(guitar, accountId);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(expectedStatus, result.StatusCode);
    }
}