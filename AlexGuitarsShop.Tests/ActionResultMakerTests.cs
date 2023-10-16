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

    [Test]
    public void ResolveResult_Result_ReturnsActionResult()
    {
        // Arrange
        string data = "Data";
        var statusCode = HttpStatusCode.OK;
        var expectedResult = new ResultDto<string>
        {
            Data = data,
            IsSuccess = true
        };

        // Act
        var actionResult = _resultBuilder.ResolveResult(new Result<string>
        {
            Data = data, IsSuccess = true, StatusCode = statusCode
        });

        var objectResult = ((OkObjectResult) actionResult.Result!).Value!;
        var result = (ResultDto<string>) objectResult;

        // Assert
        Assert.AreEqual(expectedResult.Data, result.Data);
        Assert.AreEqual(expectedResult.IsSuccess, result.IsSuccess);
    }
}