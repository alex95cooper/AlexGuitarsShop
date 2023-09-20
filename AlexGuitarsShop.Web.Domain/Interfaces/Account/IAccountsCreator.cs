using AlexGuitarsShop.Common;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Account;

public interface IAccountsCreator
{
    Task<IResult<Common.Models.Account>> AddAccountAsync(RegisterViewModel model);
}