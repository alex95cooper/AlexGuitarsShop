using AlexGuitarsShop.Domain.Interfaces;

namespace AlexGuitarsShop.Domain;

internal static class ResultCreator
{
    public static IResult<T> GetInvalidResult<T>(string message)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Error = message
        };
    }

    public static IResult<T> GetValidResult<T>(T data)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = data
        };
    }
}