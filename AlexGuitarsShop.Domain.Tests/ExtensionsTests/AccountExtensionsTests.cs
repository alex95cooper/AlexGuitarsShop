using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests;

public class AccountExtensionsTests
{
    [Test]
    public void ToAccountDto_Account_ReturnsAccountDto()
    {
        // Arrange
        var account = new Account {Name = "Alex", Email = "lex95bond@gmail.com", Role = Role.Admin};

        // Act
        var accountDto = account.ToAccountDto();

        // Assert
        Assert.AreEqual(account.Name, accountDto.Name);
        Assert.AreEqual(account.Email, accountDto.Email);
        Assert.AreEqual(account.Role, accountDto.Role);
    }

    [Test]
    public void ToAccount_AccountDto_ReturnsAccount()
    {
        // Arrange
        Role expectedRole = Role.User;
        var accountDto = new AccountDto
        {
            Name = "Anton", Email = "Anton@gmail.com", Password = "qwerty"
        };

        // Act
        var account = accountDto.ToAccount();

        // Assert
        Assert.AreEqual(accountDto.Name, account.Name);
        Assert.AreEqual(expectedRole, account.Role);
        Assert.AreEqual(accountDto.Email, account.Email);
        Assert.AreEqual(accountDto.Password, account.Password);
    }
}