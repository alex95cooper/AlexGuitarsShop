using AlexGuitarsShop.Domain.Interfaces;

namespace AlexGuitarsShop.Domain;

internal static class ResponseCreator
{
    public static IResponse<T> GetInvalidResponse<T>(string message)
    {
        return new Response<T>
        {
            StatusCode = StatusCode.InternalServerError,
            Description = message
        };
    }
    
    public static IResponse<T> GetValidResponse<T>(T data)
    {
        return new Response<T>
        {
            StatusCode = StatusCode.Ok,
            Data = data
        };
    }
}