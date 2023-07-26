using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarsCreator
{
    Task AddGuitarAsync(GuitarViewModel model);
}