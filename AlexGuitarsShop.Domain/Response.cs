using AlexGuitarsShop.Domain.Interfaces;

namespace AlexGuitarsShop.Domain;

public class Response<T> : IResponse<T>
{
    public string Description { get; init; }
    public StatusCode StatusCode { get; init; }
    public T Data { get; init; }
}