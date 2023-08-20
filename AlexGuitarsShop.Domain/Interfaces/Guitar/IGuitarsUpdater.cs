namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarsUpdater
{
    Task UpdateGuitarAsync(DAL.Models.Guitar guitar);

    Task DeleteGuitarAsync(int id);
}