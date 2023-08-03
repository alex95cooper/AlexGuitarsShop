using AlexGuitarsShop.Domain.Interfaces;

namespace AlexGuitarsShop.Domain;

public class Result<T> : IResult<T>
{
    public string Description { get; init; }
    public bool IsSuccess { get; init; }
    public T Data { get; init; }
}