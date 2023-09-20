namespace AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using GuitarDto = AlexGuitarsShop.Common.Models.Guitar;

public interface IGuitarValidator
{
    Task<bool> CheckIfGuitarExist(int id);
}