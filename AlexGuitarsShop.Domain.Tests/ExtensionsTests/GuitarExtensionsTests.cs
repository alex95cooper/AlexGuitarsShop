using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests.ExtensionsTests;

public class GuitarExtensionsTests
{
    [Test]
    public void ToGuitarDto_GuitarDal_ReturnsGuitarDto()
    {
        // Arrange
        var expectedGuitar = new Guitar
        {
            Id = 115,
            Name = "New Guitar",
            Price = 2000,
            Description = "This is a new guitar",
            Image = "base64stringImage",
            IsDeleted = 0
        };

        // Act
        var result = expectedGuitar.ToGuitarDto();

        // Assert
        Assert.AreEqual(expectedGuitar.Id, result.Id);
        Assert.AreEqual(expectedGuitar.Name, result.Name);
        Assert.AreEqual(expectedGuitar.Price, result.Price);
        Assert.AreEqual(expectedGuitar.Description, result.Description);
        Assert.AreEqual(expectedGuitar.Image, result.Image);
        Assert.AreEqual(expectedGuitar.IsDeleted, result.IsDeleted);
    }

    [Test]
    public void ToGuitarDal_GuitarDto_ReturnsGuitarDal()
    {
        // Arrange
        var expectedGuitar = new GuitarDto
        {
            Id = 115,
            Name = "New Guitar",
            Price = 2000,
            Description = "This is a new guitar",
            Image = "base64stringImage",
            IsDeleted = 0
        };

        // Act
        var result = expectedGuitar.ToGuitarDal();

        // Assert
        Assert.AreEqual(expectedGuitar.Id, result.Id);
        Assert.AreEqual(expectedGuitar.Name, result.Name);
        Assert.AreEqual(expectedGuitar.Price, result.Price);
        Assert.AreEqual(expectedGuitar.Description, result.Description);
        Assert.AreEqual(expectedGuitar.Image, result.Image);
        Assert.AreEqual(expectedGuitar.IsDeleted, result.IsDeleted);
    }
}