using System.Net;

namespace AlexGuitarsShop.Domain;

public interface IResult
{
    string Error { get; }
    bool IsSuccess { get; }
    HttpStatusCode StatusCode { get; }
}

public interface IResult<out T> : IResult
{
    T Data { get; }
}