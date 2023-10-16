using AlexGuitarsShop.Helpers;
using NUnit.Framework;

namespace AlexGuitarsShop.Tests;

public class PaginatorTests
{
    [Test]
    public void GetOffset_PageNumber_ReturnOffset()
    {
        // Arrange
        int pageNumber = 3;
        int expectedOffset = 20;

        // Act 
        int offset = Paginator.GetOffset(pageNumber);

        // Assert 
        Assert.AreEqual(expectedOffset, offset);
    }
}