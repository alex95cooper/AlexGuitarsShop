using System.Net;
using NUnit.Framework;

namespace AlexGuitarsShop.Domain.Tests;

public class ResultCreatorTests
{
    [Test]
    public void GetInvalidResult_ErrorString_ReturnResult()
    {
        // Arrange
        string message = "Goodbye World!";
        var statusCode = HttpStatusCode.BadRequest;
        
        // Act 
        var result = ResultCreator.GetInvalidResult(message, statusCode);

        // Assert 
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(statusCode, result.StatusCode);
        Assert.AreEqual(message, result.Error);
    }

    [Test]
    public void GetInvalidResultGeneric_ErrorString_ReturnResult()
    {
        // Arrange
        string message = "Goodbye World!";
        var statusCode = HttpStatusCode.BadRequest;
        
        // & Act 
        var result = ResultCreator.GetInvalidResult<int>(message, statusCode);

        // Assert 
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(statusCode, result.StatusCode);
        Assert.AreEqual(message, result.Error);
    }

    [Test]
    public void GetValidResult_TData_ReturnResult()
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