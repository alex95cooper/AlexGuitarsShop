using System.Net;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Validators;
using Moq;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests.ValidatorsTests;

public class AccountValidatorTests
{
    private Mock<IAccountRepository> _accountRepositoryMock;
    private IAccountValidator _accountValidator;

    [SetUp]
    public void Setup()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _accountValidator = new AccountValidator(_accountRepositoryMock.Object);
    }

    [Test]
    public async Task CheckIfEmailExist_Null_ReturnsInvalidResult()
    {
        // Arrange 
        var exceptedStatus = HttpStatusCode.BadRequest;
        var exceptedMessage = Constants.ErrorMessages.InvalidEmail;

        // Act
        var result = await _accountValidator.CheckIfEmailExist(null);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(exceptedStatus, result.StatusCode);
        Assert.AreEqual(exceptedMessage, result.Error);

        _accountRepositoryMock.Verify(ar =>
            ar.FindAsync(It.IsAny<string>()), Times.Never());
    }

    [Test]
    public async Task CheckIfEmailExist_InvalidInput_ReturnsValidResult()
    {
        // Arrange 
        var exceptedStatus = HttpStatusCode.BadRequest;
        string email = "lex95bond@gmail.com";

        // Act
        var result = await _accountValidator.CheckIfEmailExist(email);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(exceptedStatus, result.StatusCode);
    }

    [TestCase("TheLongestNameInTheWorld", "lex95bond@gmail.com", "asdfg")]
    [TestCase("Alex", "lex95bondgmailcom", "asdfg")]
    public void CheckIfRegisterIsValid_InvalidInput_ReturnsInvalidResult(string name, string email, string password)
    {
        // Arrange 
        var accountDto = new AccountDto {Name = name, Email = email, Password = password};
        var exceptedStatus = HttpStatusCode.BadRequest;
        var exceptedMessage = Constants.ErrorMessages.InvalidAccount;

        // Act
        var result = _accountValidator.CheckIfRegisterIsValid(accountDto);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(exceptedStatus, result.StatusCode);
        Assert.AreEqual(exceptedMessage, result.Error);
    }

    [Test]
    public void CheckIfRegisterIsValid_ValidInput_ReturnsValidResult()
    {
        // Arrange 
        var accountDto = new AccountDto {Name = "Alex", Email = "lex95bond@gmail.com", Password = "asdfg"};
        var exceptedStatus = HttpStatusCode.OK;

        // Act
        var result = _accountValidator.CheckIfRegisterIsValid(accountDto);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(exceptedStatus, result.StatusCode);
    }

    [Test]
    public void CheckIfLoginIsValid_InvalidInput_ReturnsInvalidResult()
    {
        // Arrange 
        var accountDto = new AccountDto {Email = "lex95bond@gmail.com", 
            Password = "aaaaaaaaaassssssssssddddddddddffffffffffgggggggggghh"};
        var exceptedStatus = HttpStatusCode.BadRequest;
        var exceptedMessage = Constants.ErrorMessages.InvalidAccount;

        // Act
        var result = _accountValidator.CheckIfRegisterIsValid(accountDto);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(exceptedStatus, result.StatusCode);
        Assert.AreEqual(exceptedMessage, result.Error);
    }

    [Test]
    public void CheckIfLoginIsValid_ValidInput_ReturnsValidResult()
    {
        // Arrange 
        var accountDto = new AccountDto {Email = "lex95bond@gmail.com", Password = "asdfg"};
        var exceptedStatus = HttpStatusCode.OK;

        // Act
        var result = _accountValidator.CheckIfLoginIsValid(accountDto);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(exceptedStatus, result.StatusCode);
    }
}