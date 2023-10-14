using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using NUnit.Framework;

namespace AlexGuitarsShop.Web.Domain.Tests;

public class PaginatedListExtensionsTests
{
    [TestCase(Title.Catalog, 1)]
    public void ToPaginatedListViewModel_PaginatedListDto_ReturnsViewModel(Title title, int pageNumber)
    {
        // Arrange
        PaginatedListDto<int> listDto = new PaginatedListDto<int>
        {
            CountOfAll = 3, LimitedList = new List<int> {1, 2, 3}
        };

        // Act
        var listViewModel = listDto.ToPaginatedListViewModel(title, pageNumber);

        // Assert
        Assert.AreEqual(title, listViewModel.Title);
        Assert.AreEqual(pageNumber, listViewModel.CurrentPage);
        Assert.AreEqual(listDto.LimitedList, listViewModel.List);
        Assert.AreEqual(listDto.CountOfAll, listViewModel.TotalCount);
    }
}