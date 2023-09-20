using AlexGuitarsShop.Common;
using AlexGuitarsShop.Web.Domain.ViewModels;
using AccountDto = AlexGuitarsShop.Common.Models.Account;

namespace AlexGuitarsShop.Web.Domain.Interfaces.Account;

public interface IAccountsProvider
{
    Task<IResult<AccountDto>> GetAccountAsync(LoginViewModel model);

    Task<IResult<PaginatedListViewModel<AccountDto>>> GetUsersAsync(int pageNumber);

    Task<IResult<PaginatedListViewModel<AccountDto>>> GetAdminsAsync(int pageNumber);
}