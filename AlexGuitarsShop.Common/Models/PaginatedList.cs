namespace AlexGuitarsShop.Common.Models;

public class PaginatedList<T>
{
    public int CountOfAll { get; init; }
    public List<T> LimitedList { get; init; }
}