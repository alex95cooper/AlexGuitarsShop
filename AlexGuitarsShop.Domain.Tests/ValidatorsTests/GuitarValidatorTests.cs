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
    public async Task CheckIfGuitarExist_InvalidInput_ReturnsInvalidResult()
    {
        // Arrange 
        int id = 35;
        var exceptedStatus = HttpStatusCode.BadRequest;
        var exceptedMessage = Constants.ErrorMessages.InvalidGuitarId;

        // Act
        var result = await _guitarValidator.CheckIfGuitarExist(id);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(exceptedStatus, result.StatusCode);
        Assert.AreEqual(exceptedMessage, result.Error);
    }

    [TestCase("SuperGuitar", 1000001, "asdfg")]
    public void CheckIfGuitarIsValid_InvalidInput_ReturnsInvalidResult(string name, int price, string description)
    {
        // Arrange 
        var guitarDto = new GuitarDto {Name = name, Price = price, Description = description};
        var exceptedStatus = HttpStatusCode.BadRequest;
        string exceptedMessage = Constants.ErrorMessages.InvalidGuitar;

        // Act
        var result = _guitarValidator.CheckIfGuitarIsValid(guitarDto);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(exceptedStatus, result.StatusCode);
        Assert.AreEqual(exceptedMessage, result.Error);
    }

    [TestCase("SuperGuitar", 5000, "Its description of SuperGuitar")]
    public void CheckIfGuitarIsValid_ValidInput_ReturnsValidResult(string name, int price, string description)
    {
        // Arrange 
        var guitarDto = new GuitarDto {Name = name, Price = price, Description = description};
        var exceptedStatus = HttpStatusCode.OK;

        // Act
        var result = _guitarValidator.CheckIfGuitarIsValid(guitarDto);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(exceptedStatus, result.StatusCode);
    }

    [Test]
    public async Task CheckIfGuitarUpdateIsValid_InvalidInput_ReturnsInvalidResult()
    {
        // Arrange 
        var guitarDto = new GuitarDto {Id = 35};
        var exceptedStatus = HttpStatusCode.BadRequest;
        var exceptedMessage = Constants.ErrorMessages.InvalidGuitar;

        // Act
        var result = await _guitarValidator.CheckIfGuitarUpdateIsValid(guitarDto);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(exceptedStatus, result.StatusCode);
        Assert.AreEqual(exceptedMessage, result.Error);
    }
}