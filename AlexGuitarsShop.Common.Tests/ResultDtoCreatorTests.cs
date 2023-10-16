using NUnit.Framework;

namespace AlexGuitarsShop.Common.Tests;

public class ResultDtoCreatorTests
{
    [Test]
    public void GetInvalidResultGeneric_ErrorString_ReturnsResult()
    {
        // Arrange
        string errorMessage = "Goodbye World!";
        
        // Act 
        var result = ResultDtoCreator.GetInvalidResult<int>(errorMessage);

        // Assert 
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(errorMessage, result.Error);
    }
    
    [Test]
    public void GetInvalidResult_ErrorString_ReturnsResult()
    {
        // Arrange
        string errorMessage = "Goodbye World!";
        
        //  Act 
        var result = ResultDtoCreator.GetInvalidResult(errorMessage);

        // Assert 
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(errorMessage, result.Error);
    }
    
    [Test]
    public void GetValidResult_TData_ReturnResult()
    {
        // Arrange
        string data = "Data";
        
        // Act 
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