using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Creators;
using AlexGuitarsShop.Domain.Interfaces.Account;
using Moq;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests;

public class AccountsCreatorTests
{
    private Mock<IAccountRepository> _accountRepositoryMock;
    private IAccountsCreator _accountsCreator;

    [SetUp]
    public void Setup()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _accountsCreator = new AccountsCreator(_accountRepositoryMock.Object);
    }

    [Test]
    public async Task AddAccountAsync_ValidAccountDto_ReturnsValidResult()
    {
        // Arrange
        var account = new AccountDto
        {
            Name = "Alex",
            Email = "lex95bond@gmail.com", Password = "asdfg", Role = Role.Admin
        };

        // Act
        var result = await _accountsCreator.AddAccountAsync(account);

        // Assert
        Assert.AreEqual(account.Name, result.Data.Name);
        Assert.AreEqual(account.Email, result.Data.Email);
        Assert.AreEqual(account.Role, result.Data.Role);
    }
}