namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarsUpdater
{
    Task UpdateGuitarAsync(Common.Models.Guitar guitar);

    Task DeleteGuitarAsync(int id);
}