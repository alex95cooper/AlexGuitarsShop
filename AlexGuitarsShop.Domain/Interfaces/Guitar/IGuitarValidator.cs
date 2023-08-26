namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarValidator
{
    Task<bool> CheckIfGuitarExist(int id);

    Task<bool> CheckIfPageIsValid(int pageNumber, int limit);

    bool CheckIfGuitarIsValid(DAL.Models.Guitar guitar);
}