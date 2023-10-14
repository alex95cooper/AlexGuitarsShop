using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests;

public class CartItemExtensionsTests
{
    [Test]
    public void ToCartItemDto_CartItem_ReturnsCartItemDto()
    {
        // Arrange
        var expectedItem = new CartItem
        {
            ProductId = 35,
            Quantity = 5, Product = new Guitar {Id = 1}
        };

        // Act
        var result = expectedItem.ToCartItemDto();

        // Assert
        Assert.AreEqual(expectedItem.ProductId, result.ProductId);
        Assert.AreEqual(expectedItem.Quantity, result.Quantity);
    }
}