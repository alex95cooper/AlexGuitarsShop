using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Account;

public interface IAccountsCreator
{
    Task<IResult<AccountDto>> AddAccountAsync(RegisterViewModel model);
}