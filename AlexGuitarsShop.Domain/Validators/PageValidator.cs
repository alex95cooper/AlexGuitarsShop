namespace AlexGuitarsShop.Domain.Validators;

public static class PageValidator
{
    public static bool CheckIfPageIsValid(int pageNumber, int limit, int totalCount)
    {
        if (totalCount < limit)
        {
            return pageNumber == 1;
        }

        int pageCount = totalCount / limit;
        pageCount = totalCount % limit > 0 ? pageCount + 1 : pageCount;
        return pageNumber > 0 && pageNumber <= pageCount;
    }
}