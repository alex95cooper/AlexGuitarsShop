using AlexGuitarsShop.Common;
using AlexGuitarsShop.Web.Domain.ViewModels;
using GuitarDto = AlexGuitarsShop.Common.Models.Guitar;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Guitar;

public interface IGuitarsCreator
{
    Task<IResult<GuitarDto>> AddGuitarAsync(GuitarViewModel model);
}