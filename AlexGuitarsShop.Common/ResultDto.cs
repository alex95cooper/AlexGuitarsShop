namespace AlexGuitarsShop.Common;

public class ResultDto : IResultDto
{
    public string Error { get; init; }
    public bool IsSuccess { get; init; }
}

public class ResultDto<T> : ResultDto, IResultDto<T>
{
    public T Data { get; init; }
}