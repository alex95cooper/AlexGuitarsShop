using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Providers;
using Moq;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests;

public class AccountsProviderTests
{
    private Mock<IAccountRepository> _accountRepositoryMock;
    private IAccountsProvider _accountsProvider;

    [SetUp]
    public void Setup()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _accountsProvider = new AccountsProvider(_accountRepositoryMock.Object);
    }

    [Test]
    public async Task GetAccountAsync_ValidAccountDto_ReturnsInvalidResult()
    {
        // Arrange
        var account = new AccountDto
        {
            Name = "Alex",
            Email = "lex95bond@gmail.com", Password = "asdfg"
        };
        string expectedMessage = Constants.ErrorMessages.UserNotFound;

        // Act
        var result = await _accountsProvider.GetAccountAsync(account);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(expectedMessage, result.Error);
    }
}