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
        var expectedStatusCode = HttpStatusCode.BadRequest;
        var expectedMessage = Constants.ErrorMessages.InvalidEmail;

        // Act
        var result = await _accountValidator.CheckIfEmailExist(null);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
        Assert.AreEqual(expectedMessage, result.Error);

        _accountRepositoryMock.Verify(ar =>
            ar.FindAsync(It.IsAny<string>()), Times.Never());
    }

    [Test]
    public async Task CheckIfEmailExist_InvalidEmail_ReturnsValidResult()
    {
        // Arrange 
        var expectedStatusCode = HttpStatusCode.BadRequest;
        string email = "Valera@gmail.com";

        // Act
        var result = await _accountValidator.CheckIfEmailExist(email);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
    }

    [Test]
    public void CheckIfRegisterIsValid_InvalidEmail_ReturnsInvalidResult()
    {
        // Arrange 
        var accountDto = new AccountDto
        {
            Name = "Alex",
            Email = "invalidEmail",
            Password = "asdfg"
        };
        var expectedStatusCode = HttpStatusCode.BadRequest;
        var expectedMessage = Constants.ErrorMessages.InvalidAccount;

        // Act
        var result = _accountValidator.CheckIfRegisterIsValid(accountDto);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
        Assert.AreEqual(expectedMessage, result.Error);
    }

    [Test]
    public void CheckIfRegisterIsValid_ValidAccount_ReturnsValidResult()
    {
        // Arrange 
        var accountDto = new AccountDto
        {
            Name = "Alex",
            Email = "lex95bond@gmail.com",
            Password = "asdfg"
        };
        var expectedStatusCode = HttpStatusCode.OK;

        // Act
        var result = _accountValidator.CheckIfRegisterIsValid(accountDto);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
    }

    [Test]
    public void CheckIfLoginIsValid_InvalidPassword_ReturnsInvalidResult()
    {
        // Arrange 
        var accountDto = new AccountDto
        {
            Email = "lex95bond@gmail.com",
            Password = new string('a', 51)
        };
        var expectedStatusCode = HttpStatusCode.BadRequest;
        var expectedMessage = Constants.ErrorMessages.InvalidAccount;

        // Act
        var result = _accountValidator.CheckIfRegisterIsValid(accountDto);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
        Assert.AreEqual(expectedMessage, result.Error);
    }

    [Test]
    public void CheckIfLoginIsValid_ValidAccount_ReturnsValidResult()
    {
        // Arrange 
        var accountDto = new AccountDto
        {
            Email = "lex95bond@gmail.com",
            Password = "asdfg"
        };
        var expectedStatusCode = HttpStatusCode.OK;

        // Act
        var result = _accountValidator.CheckIfLoginIsValid(accountDto);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
    }
}