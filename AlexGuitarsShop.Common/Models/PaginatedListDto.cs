namespace AlexGuitarsShop.Common.Models;

public class PaginatedListDto<T>
{
    public int CountOfAll { get; init; }
    public List<T> LimitedList { get; init; }
}