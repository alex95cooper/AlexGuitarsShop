namespace AlexGuitarsShop.Common;

public static class ResultDtoCreator
{
    public static ResultDto<T> GetInvalidResult<T>(string message)
    {
        return new ResultDto<T>
        {
            IsSuccess = false,
            Error = message
        };
    }

    public static ResultDto<T> GetValidResult<T>(T data)
    {
        return new ResultDto<T>
        {
            IsSuccess = true,
            Data = data
        };
    }
}