using NUnit.Framework;

namespace AlexGuitarsShop.Common.Tests;

public class ResultDtoCreatorTests
{
    [TestCase("Goodbye World!")]
    public void GetInvalidResult_ErrorString_ReturnResult(string message)
    {
        // Arrange & Act 
        var result = ResultDtoCreator.GetInvalidResult(message);

        // Assert 
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(message, result.Error);
    }

    [TestCase("Goodbye World!")]
    public void GetInvalidResult_ErrorString_ReturnResult<T>(string message)
    {
        // Arrange & Act 
        var result = ResultDtoCreator.GetInvalidResult<T>(message);

        // Assert 
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(message, result.Error);
    }

    [TestCase("Data")]
    public void GetValidResult_TData_ReturnResult<T>(T data)
    {
        // Arrange & Act 
        var result = ResultDtoCreator.GetValidResult(data);

        // Assert 
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(data, result.Data);
    }

    [Test]
    public void GetValidResult_Empty_ReturnResult()
    {
        // Arrange & Act 
        var result = ResultDtoCreator.GetValidResult();

        // Assert 
        Assert.IsTrue(result.IsSuccess);
    }
}