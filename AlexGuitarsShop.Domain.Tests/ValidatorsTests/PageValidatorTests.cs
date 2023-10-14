using AlexGuitarsShop.Domain.Validators;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests.ValidatorsTests;

public class PageValidatorTests
{
    [TestCase(5, 10, 35)]
    [TestCase(6, 10, 45)]
    public void CheckIfPageIsValid_InvalidInput_ReturnsFalse(int pageNumber, int limit, int totalCount)
    {
        // Arrange & Act
        bool result = PageValidator.CheckIfPageIsValid(pageNumber, limit, totalCount);

        // Assert
        Assert.IsFalse(result);
    }

    [TestCase(6, 10, 55)]
    public void CheckIfPageIsValid_ValidInput_ReturnsTrue(int pageNumber, int limit, int totalCount)
    {
        // Arrange & Act
        bool result = PageValidator.CheckIfPageIsValid(pageNumber, limit, totalCount);

        // Assert
        Assert.IsTrue(result);
    }
}