using System.Net;

namespace AlexGuitarsShop.Common;

public class Result<T> : IResult<T>
{
    public string Error { get; init; }
    public bool IsSuccess { get; init; }
    public HttpStatusCode StatusCode { get; init; }
    public T Data { get; init; }
}