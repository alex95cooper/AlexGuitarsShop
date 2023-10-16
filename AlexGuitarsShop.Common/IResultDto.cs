namespace AlexGuitarsShop.Common;

public interface IResultDto
{
    string Error { get; }
    bool IsSuccess { get; }
}

public interface IResultDto<out T> : IResultDto
{
    T Data { get; }
}