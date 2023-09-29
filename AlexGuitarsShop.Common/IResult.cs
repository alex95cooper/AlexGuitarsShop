using System.Net;

namespace AlexGuitarsShop.Common;

public interface IResult<out T>
{
    string Error { get; }
    bool IsSuccess { get; }
    HttpStatusCode StatusCode { get; init; }
    T Data { get; }
}