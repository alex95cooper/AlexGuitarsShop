using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.ViewModels;
using NUnit.Framework;

namespace AlexGuitarsShop.Web.Domain.Tests;

public class GuitarExtensionsTest
{
    [Test]
    public void ToGuitar_ViewModel_ReturnsGuitarDto()
    {
        // Arrange
        var model = new GuitarViewModel
        {
            Id = 115,
            Name = "New Guitar",
            Price = 2000,
            Description = "This is a new guitar",
            Image = "base64stringImage",
            IsDeleted = 0
        };

        // Act
        var guitar = model.ToGuitarDto();

        // Assert
        Assert.AreEqual(model.Id, guitar.Id);
        Assert.AreEqual(model.Name, guitar.Name);
        Assert.AreEqual(model.Price, guitar.Price);
        Assert.AreEqual(model.Description, guitar.Description);
        Assert.AreEqual(model.Image, guitar.Image);
        Assert.AreEqual(model.IsDeleted, guitar.IsDeleted);
    }

    [Test]
    public void ToGuitar_GuitarDto_ReturnsViewModel()
    {
        // Arrange
        var guitar = new GuitarDto
        {
            Id = 115,
            Name = "New Guitar",
            Price = 2000,
            Description = "This is a new guitar",
            Image = "base64stringImage",
            IsDeleted = 0
        };

        // Act
        var model = guitar.ToGuitarViewModel();

        // Assert
        Assert.AreEqual(guitar.Id, model.Id);
        Assert.AreEqual(guitar.Name, model.Name);
        Assert.AreEqual(guitar.Price, model.Price);
        Assert.AreEqual(guitar.Description, model.Description);
        Assert.AreEqual(guitar.Image, model.Image);
        Assert.AreEqual(guitar.IsDeleted, model.IsDeleted);
    }
}