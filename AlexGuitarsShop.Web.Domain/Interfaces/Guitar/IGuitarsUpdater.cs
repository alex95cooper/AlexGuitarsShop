using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Guitar;

public interface IGuitarsUpdater
{
    Task<IResultDto> UpdateGuitarAsync(GuitarViewModel model);

    Task<IResultDto> DeleteGuitarAsync(int id);
}