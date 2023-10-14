using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Helpers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace AlexGuitarsShop.Tests;

public class ActionResultMakerTests
{
    private ActionResultBuilder _resultBuilder;

    [SetUp]
    public void Setup()
    {
        _resultBuilder = new ActionResultBuilder();
    }

    [TestCase("Data", HttpStatusCode.OK)]
    public void ResolveResult_Result_ReturnsActionResult<T>(T data, HttpStatusCode statusCode)
    {
        // Arrange
        var expectedResult = new ResultDto<T> {Data = data, IsSuccess = true};

        // Act
        var actionResult = _resultBuilder.ResolveResult(new Result<T>
        {
            Data = data, IsSuccess = true, StatusCode = statusCode
        });

        var objectResult = ((OkObjectResult) actionResult.Result!).Value!;
        var result = (ResultDto<T>) objectResult;

        // Assert
        Assert.AreEqual(expectedResult.Data, result.Data);
        Assert.AreEqual(expectedResult.IsSuccess, result.IsSuccess);
    }
}