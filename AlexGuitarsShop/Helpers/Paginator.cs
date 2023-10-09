namespace AlexGuitarsShop.Helpers;

public static class Paginator
{
    public const int Limit = 10;
    
    public static int GetOffset(int pageNumber)
    {
        return (pageNumber - 1) * Limit;
    }
}