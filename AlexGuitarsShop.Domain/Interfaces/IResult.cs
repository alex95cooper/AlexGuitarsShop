namespace AlexGuitarsShop.Domain.Interfaces;

public interface IResult<out T>
{
    string Description { get; }
    bool IsSuccess { get; }
    T Data { get; }
}