using System.Net;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Domain.Updaters;
using Moq;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests;

public class CartItemsUpdaterTests
{
    private Mock<ICartItemRepository> _cartItemRepositoryMock;
    private ICartItemsUpdater _cartItemsUpdater;

    [SetUp]
    public void Setup()
    {
        _cartItemRepositoryMock = new Mock<ICartItemRepository>();
        _cartItemsUpdater = new CartItemsUpdater(_cartItemRepositoryMock.Object);
    }

    [Test]
    public async Task IncrementAsync_ValidInput_ReturnsValidResult()
    {
        // Arrange 
        int id = 5;
        int accountId = 1;
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var result = await _cartItemsUpdater.IncrementAsync(id, accountId);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(expectedStatus, result.StatusCode);

        _cartItemRepositoryMock.Verify(crm =>
            crm.UpdateQuantityAsync(It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<int>()), Times.Never());
    }

    [Test]
    public async Task DecrementAsync_ValidInput_ReturnsValidResult()
    {
        // Arrange 
        int id = 5;
        int accountId = 1;
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var result = await _cartItemsUpdater.DecrementAsync(id, accountId);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(expectedStatus, result.StatusCode);

        _cartItemRepositoryMock.Verify(crm =>
            crm.UpdateQuantityAsync(It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<int>()), Times.Never());
    }
}