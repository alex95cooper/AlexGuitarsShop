using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Guitar;

public interface IGuitarsUpdater
{
    Task<IResultDto<GuitarDto>> UpdateGuitarAsync(GuitarViewModel model);

    Task<IResultDto<int>> DeleteGuitarAsync(int id);
}