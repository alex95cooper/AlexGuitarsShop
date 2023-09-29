namespace AlexGuitarsShop.Web.Domain.Interfaces.Guitar;

public interface IGuitarValidator
{
    Task<bool> CheckIfGuitarExist(int id);
}