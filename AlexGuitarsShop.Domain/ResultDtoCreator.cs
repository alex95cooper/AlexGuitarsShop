using System.Net;

namespace AlexGuitarsShop.Domain;

public static class ResultCreator
{
    public static Result<T> GetInvalidResult<T>(string message, HttpStatusCode status)
    {
        return new Result<T>
        {
            StatusCode = status,
            IsSuccess = false,
            Error = message
        };
    }

    public static Result<T> GetValidResult<T>(T data, HttpStatusCode status)
    {
        return new Result<T>
        {
            StatusCode = status,
            IsSuccess = true,
            Data = data
        };
    }
}