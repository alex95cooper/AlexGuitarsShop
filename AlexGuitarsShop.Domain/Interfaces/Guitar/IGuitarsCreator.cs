namespace AlexGuitarsShop.Domain.Interfaces.Guitar;

public interface IGuitarsCreator
{
    Task AddGuitarAsync(Common.Models.Guitar guitar);
}