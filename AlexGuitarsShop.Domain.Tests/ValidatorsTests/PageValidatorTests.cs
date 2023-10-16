using AlexGuitarsShop.Domain.Validators;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests.ValidatorsTests;

public class PageValidatorTests
{
    [Test]
    public void CheckIfPageIsValid_InvalidPageNumber_ReturnsFalse()
    {
        // Arrange
        int pageNumber = 5;
        int limit = 10;
        int totalCount = 35;

        // Act
        bool result = PageValidator.CheckIfPageIsValid(pageNumber, limit, totalCount);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void CheckIfPageIsValid_ValidInput_ReturnsTrue()
    {
        // Arrange
        int pageNumber = 6;
        int limit = 10;
        int totalCount = 55;

        // Act
        bool result = PageValidator.CheckIfPageIsValid(pageNumber, limit, totalCount);

        // Assert
        Assert.IsTrue(result);
    }
}