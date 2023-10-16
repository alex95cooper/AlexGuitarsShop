using System.Net;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.Validators;
using Moq;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests.ValidatorsTests;

public class GuitarValidatorTests
{
    private Mock<IGuitarRepository> _guitarRepositoryMock;
    private IGuitarValidator _guitarValidator;

    [SetUp]
    public void Setup()
    {
        _guitarRepositoryMock = new Mock<IGuitarRepository>();
        _guitarValidator = new GuitarValidator(_guitarRepositoryMock.Object);
    }

    [Test]
    public async Task CheckIfGuitarExist_InvalidId_ReturnsInvalidResult()
    {
        // Arrange 
        int id = 35;
        var expectedStatusCode = HttpStatusCode.BadRequest;
        var expectedMessage = Constants.ErrorMessages.InvalidGuitarId;

        // Act
        var result = await _guitarValidator.CheckIfGuitarExist(id);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
        Assert.AreEqual(expectedMessage, result.Error);
    }

    [Test]
    public void CheckIfGuitarIsValid_InvalidPrice_ReturnsInvalidResult()
    {
        // Arrange
        var guitarDto = new GuitarDto
        {
            Name = "SuperGuitar",
            Price = 1000001,
            Description = "asdfg"
        };
        var expectedStatusCode = HttpStatusCode.BadRequest;
        string expectedMessage = Constants.ErrorMessages.InvalidGuitar;

        // Act
        var result = _guitarValidator.CheckIfGuitarIsValid(guitarDto);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
        Assert.AreEqual(expectedMessage, result.Error);
    }

    [Test]
    public void CheckIfGuitarIsValid_ValidGuitarDto_ReturnsValidResult()
    {
        // Arrange 
        var guitarDto = new GuitarDto
        {
            Name = "SuperGuitar",
            Price = 5000,
            Description = "Its description of SuperGuitar"
        };
        var expectedStatusCode = HttpStatusCode.OK;

        // Act
        var result = _guitarValidator.CheckIfGuitarIsValid(guitarDto);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
    }

    [Test]
    public async Task CheckIfGuitarUpdateIsValid_InvalidId_ReturnsInvalidResult()
    {
        // Arrange 
        var guitarDto = new GuitarDto
        {
            Id = 35
        };
        var expectedStatusCode = HttpStatusCode.BadRequest;
        var expectedMessage = Constants.ErrorMessages.InvalidGuitar;

        // Act
        var result = await _guitarValidator.CheckIfGuitarUpdateIsValid(guitarDto);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
        Assert.AreEqual(expectedMessage, result.Error);
    }
}