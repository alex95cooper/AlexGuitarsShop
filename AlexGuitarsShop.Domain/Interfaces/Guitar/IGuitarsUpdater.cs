using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarsUpdater
{
    Task UpdateGuitarAsync(GuitarViewModel model);

    Task DeleteGuitarAsync(int id);
}