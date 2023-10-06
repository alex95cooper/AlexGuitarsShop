namespace AlexGuitarsShop.Common;

public class ResultDto<T> : IResultDto<T>
{
    public string Error { get; init; }
    public bool IsSuccess { get; init; }
    public T Data { get; init; }
}