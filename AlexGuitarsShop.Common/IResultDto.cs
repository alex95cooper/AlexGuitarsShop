namespace AlexGuitarsShop.Common;

public interface IResultDto<out T>
{
    string Error { get; }
    bool IsSuccess { get; }
    T Data { get; }
}