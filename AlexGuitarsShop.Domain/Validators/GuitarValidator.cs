using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces.Guitar;

namespace AlexGuitarsShop.Domain.Validators;

public class GuitarValidator : IGuitarValidator
{
    private readonly IGuitarRepository _guitarRepository;

    public GuitarValidator(IGuitarRepository guitarRepository)
    {
        _guitarRepository = guitarRepository;
    }

    public async Task<bool> CheckIfGuitarExist(int id)
    {
        return await _guitarRepository.GetAsync(id) != null;
    }

    public async Task<bool> CheckIfPageIsValid(int pageNumber, int limit)
    {
        int count = await _guitarRepository.GetCountAsync();
        int pageCount = GetPageCount(count, limit);
        return pageNumber > 0 && pageNumber <= pageCount;
    }

    public bool CheckIfGuitarIsValid(Guitar guitar)
    {
        guitar = guitar ?? throw new ArgumentNullException(nameof(guitar));
        return CheckIfNameIsValid(guitar.Name)
               && CheckIfPriceIsValid(guitar.Price)
               && CheckIfDescriptionIsValid(guitar.Description);
    }

    private static int GetPageCount(int count, int limit)
    {
        if (count < limit)
        {
            return count == 0 ? 0 : 1;
        }

        int pageCount = count / limit;
        return count % limit > 0 ? pageCount + 1 : pageCount;
    }

    private bool CheckIfNameIsValid(string name)
    {
        name = name ?? throw new ArgumentNullException(nameof(name));
        return name.Length is > 5 and < 50;
    }

    private bool CheckIfPriceIsValid(int price)
    {
        return price is > 10 and < 1000000;
    }

    private bool CheckIfDescriptionIsValid(string description)
    {
        description = description ?? throw new ArgumentNullException(nameof(description));
        return description.Length is > 10 and < 600;
    }
}