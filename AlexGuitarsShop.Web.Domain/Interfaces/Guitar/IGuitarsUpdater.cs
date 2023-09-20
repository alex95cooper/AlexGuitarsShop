using AlexGuitarsShop.Common;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Guitar;

public interface IGuitarsUpdater
{
    Task<IResult<Common.Models.Guitar>> UpdateGuitarAsync(GuitarViewModel model);

    Task<IResult<int>> DeleteGuitarAsync(int id);
}