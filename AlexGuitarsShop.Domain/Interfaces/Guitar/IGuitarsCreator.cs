namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarsCreator
{
    Task AddGuitarAsync(DAL.Models.Guitar guitar);
}