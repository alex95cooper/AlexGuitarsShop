namespace AlexGuitarsShop.Domain.Interfaces;

public interface IResult<out T>
{
    string Error { get; }
    bool IsSuccess { get; }
    T Data { get; }
}