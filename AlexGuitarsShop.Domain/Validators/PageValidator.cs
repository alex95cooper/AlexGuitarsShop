namespace AlexGuitarsShop.Domain.Validators;

public static class PageValidator
{
    public static bool CheckIfPageIsValid(int pageNumber, int limit, int totalCount)
    {
        double pagesCount = Math.Ceiling((double)totalCount / limit);
        return pageNumber > 0 && pageNumber <= pagesCount;
    }
}