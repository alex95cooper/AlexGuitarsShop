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

    [Test]
    public void CheckIfGuitarIsValid_InvalidInput_ReturnsInvalidResult()
    {
        // Arrange
        var guitarDto = new GuitarDto {Name = "SuperGuitar", Price = 1000001, Description = "asdfg"};
        var exceptedStatus = HttpStatusCode.BadRequest;
        string exceptedMessage = Constants.ErrorMessages.InvalidGuitar;

        // Act
        var result = _guitarValidator.CheckIfGuitarIsValid(guitarDto);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(exceptedStatus, result.StatusCode);
        Assert.AreEqual(exceptedMessage, result.Error);
    }

    [Test]
    public void CheckIfGuitarIsValid_ValidInput_ReturnsValidResult()
    {
        // Arrange 
        var guitarDto = new GuitarDto {Name = "SuperGuitar", Price = 5000, 
            Description = "Its description of SuperGuitar"};
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