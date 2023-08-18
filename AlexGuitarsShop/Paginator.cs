using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop;

public class Paginator
{
    private readonly int _limit;

    private int _pageCount;

    public Paginator(int limit)
    {
        _limit = limit;
    }
    
    public int OffSet { get; private set; }

    public void SetPaginationValues(int pageNumber, IResult<int> countResult)
    {
        countResult = countResult ?? throw new ArgumentNullException(nameof(countResult));
        int count = countResult.Data;
        OffSet = (pageNumber - 1) * _limit;
        _pageCount = count % _limit == 0 ? count / _limit : count / _limit + 1;
    }

    public ListViewModel<T> GetPaginatedList<T>(List<T> list, Title title, int pageNumber)
    {
        return new ListViewModel<T>
        {
            List = list, Title = Title.Admins,
            PageCount = _pageCount, CurrentPage = pageNumber
        };
    }
}