using System.Net;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests;

public class ResultCreatorTests
{
    [TestCase("Goodbye World!", HttpStatusCode.BadRequest)]
    public void GetInvalidResult_ErrorString_ReturnResult(string message, HttpStatusCode statusCode)
    {
        // Arrange & Act 
        var result = ResultCreator.GetInvalidResult(message, statusCode);

        // Assert 
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(statusCode, result.StatusCode);
        Assert.AreEqual(message, result.Error);
    }

    [TestCase("Goodbye World!", HttpStatusCode.BadRequest)]
    public void GetInvalidResult_ErrorString_ReturnResult<T>(string message, HttpStatusCode statusCode)
    {
        // Arrange & Act 
        var result = ResultCreator.GetInvalidResult<T>(message, statusCode);

        // Assert 
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(statusCode, result.StatusCode);
        Assert.AreEqual(message, result.Error);
    }

    [TestCase("Data", HttpStatusCode.BadRequest)]
    public void GetValidResult_TData_ReturnResult<T>(T data, HttpStatusCode statusCode)
    {
        // Arrange & Act 
        var result = ResultCreator.GetValidResult(data, statusCode);

        // Assert 
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(data, result.Data);
    }

    [Test]
    public void GetValidResult_Empty_ReturnResult()
    {
        // Arrange
        HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

        // Act 
        var result = ResultCreator.GetValidResult();


        // Assert 
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);
    }
}