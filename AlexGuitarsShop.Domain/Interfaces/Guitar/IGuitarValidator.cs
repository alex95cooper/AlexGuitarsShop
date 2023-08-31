namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarValidator
{
    Task<bool> CheckIfGuitarExist(int id);
    
    bool CheckIfGuitarIsValid(DAL.Models.Guitar guitar);

    Task<bool> CheckIfGuitarUpdateIsValid(DAL.Models.Guitar guitar);
}