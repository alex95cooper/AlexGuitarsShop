using System.Net;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests;

public class ResultCreatorTests
{
    [Test]
    public void GetInvalidResult_ErrorString_ReturnInvalidResult()
    {
        // Arrange
        string message = "Goodbye World!";
        var expectedStatusCode = HttpStatusCode.BadRequest;

        // Act 
        var result = ResultCreator.GetInvalidResult(message, expectedStatusCode);

        // Assert 
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
        Assert.AreEqual(message, result.Error);
    }

    [Test]
    public void GetInvalidResultGeneric_ErrorString_ReturnInvalidResult()
    {
        // Arrange
        string message = "Goodbye World!";
        var expectedStatusCode = HttpStatusCode.BadRequest;

        // & Act 
        var result = ResultCreator.GetInvalidResult<int>(message, expectedStatusCode);

        // Assert 
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
        Assert.AreEqual(message, result.Error);
    }

    [Test]
    public void GetValidResult_DataString_ReturnValidResult()
    {
        // Arrange
        string data = "Data";
        var statusCode = HttpStatusCode.BadRequest;

        // Act 
        var result = ResultCreator.GetValidResult(data, statusCode);

        // Assert 
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(data, result.Data);
    }

    [Test]
    public void GetValidResult_Void_ReturnValidResult()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;

        // Act 
        var result = ResultCreator.GetValidResult();


        // Assert 
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
    }
}