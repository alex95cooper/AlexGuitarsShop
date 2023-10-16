using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.ViewModels;
using NUnit.Framework;

namespace AlexGuitarsShop.Web.Domain.Tests;

public class AccountExtensionsTests
{
    [Test]
    public void ToAccountDto_LoginViewModel_ReturnsAccountDto()
    {
        // Arrange
        var model = new LoginViewModel
        {
            Email = "lex95bond@gmail.com",
            Password = "asdfg"
        };

        // Act
        var account = model.ToAccountDto();

        // Assert
        Assert.AreEqual(model.Email, account.Email);
        Assert.AreEqual(model.Password, account.Password);
    }

    [Test]
    public void ToAccountDto_RegisterViewModel_ReturnsAccountDto()
    {
        // Arrange
        var model = new RegisterViewModel
        {
            Name = "Anton", 
            Email = "Anton@gmail.com",
            Password = "qwerty", 
            PasswordConfirm = "qwerty"
        };

        // Act
        var account = model.ToAccountDto();

        // Assert
        Assert.AreEqual(model.Name, account.Name);
        Assert.AreEqual(model.Email, account.Email);
        Assert.AreEqual(model.Password, account.Password);
    }
}