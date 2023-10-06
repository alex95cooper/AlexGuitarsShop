using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Guitar;

public interface IGuitarsCreator
{
    Task<IResultDto<GuitarDto>> AddGuitarAsync(GuitarViewModel model);
}