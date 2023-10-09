using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Account;

public interface IAccountsProvider
{
    Task<IResultDto<AccountDto>> GetAccountAsync(LoginViewModel model);

    Task<IResultDto<PaginatedListViewModel<AccountDto>>> GetUsersAsync(int pageNumber);

    Task<IResultDto<PaginatedListViewModel<AccountDto>>> GetAdminsAsync(int pageNumber);
}