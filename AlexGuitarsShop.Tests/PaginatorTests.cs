using AlexGuitarsShop.Helpers;
using NUnit.Framework;

namespace AlexGuitarsShop.Tests;

public class PaginatorTests
{
    [TestCase(3, 20)]
    public void GetOffset_PageNumber_ReturnOffset(int pageNumber, int expectedOffset)
    {
        // Arrange & Act 
        int offset = Paginator.GetOffset(pageNumber);

        // Assert 
        Assert.AreEqual(expectedOffset, offset);
    }
}