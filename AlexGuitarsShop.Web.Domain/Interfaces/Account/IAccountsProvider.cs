using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Account;

public interface IAccountsProvider
{
    Task<IResult<AccountDto>> GetAccountAsync(LoginViewModel model);

    Task<IResult<PaginatedListViewModel<AccountDto>>> GetUsersAsync(int pageNumber);

    Task<IResult<PaginatedListViewModel<AccountDto>>> GetAdminsAsync(int pageNumber);
}