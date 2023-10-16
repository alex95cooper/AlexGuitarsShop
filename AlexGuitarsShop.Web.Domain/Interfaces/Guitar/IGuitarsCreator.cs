using AlexGuitarsShop.Common;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Guitar;

public interface IGuitarsCreator
{
    Task<IResultDto> AddGuitarAsync(GuitarViewModel model);
}