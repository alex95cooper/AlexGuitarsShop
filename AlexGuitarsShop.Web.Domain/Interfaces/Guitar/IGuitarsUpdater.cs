using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Guitar;

public interface IGuitarsUpdater
{
    Task<IResult<GuitarDto>> UpdateGuitarAsync(GuitarViewModel model);

    Task<IResult<int>> DeleteGuitarAsync(int id);
}