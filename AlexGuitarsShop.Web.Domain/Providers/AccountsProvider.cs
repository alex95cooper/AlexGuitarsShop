using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Account;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Providers;

public class AccountsProvider : IAccountsProvider
{
    private readonly IResponseMaker _responseMaker;

    public AccountsProvider(IResponseMaker responseMaker)
    {
        _responseMaker = responseMaker;
    }

    public async Task<IResult<AccountDto>> GetAccountAsync(LoginViewModel model)
    {
        if (model == null)
        {
            return ResultCreator.GetInvalidResult<AccountDto>(
                Constants.Account.IncorrectAccount, HttpStatusCode.BadRequest);
        }

        AccountDto accountDto = model.ToAccountDto();
        return await _responseMaker.PostAsync(accountDto, Constants.Routes.Login);
    }

    public async Task<IResult<PaginatedListViewModel<AccountDto>>> GetAdminsAsync(int pageNumber)
    {
        var result = await _responseMaker.GetListByLimitAsync<AccountDto>(Constants.Routes.Admins, pageNumber);
        return result is {IsSuccess: true}
            ? ResultCreator.GetValidResult(result.Data.ToPaginatedListViewModel(Title.Admins, pageNumber),
                result.StatusCode)
            : ResultCreator.GetInvalidResult<PaginatedListViewModel<AccountDto>>(result!.Error, result.StatusCode);
    }

    public async Task<IResult<PaginatedListViewModel<AccountDto>>> GetUsersAsync(int pageNumber)
    {
        var result = await _responseMaker.GetListByLimitAsync<AccountDto>(Constants.Routes.Users, pageNumber);
        return result is {IsSuccess: true}
            ? ResultCreator.GetValidResult(result.Data.ToPaginatedListViewModel(Title.Users, pageNumber),
                result.StatusCode)
            : ResultCreator.GetInvalidResult<PaginatedListViewModel<AccountDto>>(result!.Error, result.StatusCode);
    }
}