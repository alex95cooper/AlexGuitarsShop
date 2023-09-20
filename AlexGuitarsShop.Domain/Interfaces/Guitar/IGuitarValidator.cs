namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarValidator
{
    Task<bool> CheckIfGuitarExist(int id);
    
    bool CheckIfGuitarIsValid(Common.Models.Guitar guitar);

    Task<bool> CheckIfGuitarUpdateIsValid(Common.Models.Guitar guitar);
}