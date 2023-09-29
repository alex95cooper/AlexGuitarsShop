using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Interfaces.Account;

namespace AlexGuitarsShop.Web.Domain.Updaters;

public class AccountsUpdater : IAccountsUpdater
{
    private readonly IResponseMaker _responseMaker;

    public AccountsUpdater(IResponseMaker responseMaker)
    {
        _responseMaker = responseMaker;
    }

    public async Task<IResult<AccountDto>> SetAdminRightsAsync(string email)
    {
        AccountDto accountDto = new AccountDto {Email = email};
        return await _responseMaker.PutAsync(accountDto, Constants.Routes.MakeAdmin);
    }

    public async Task<IResult<AccountDto>> RemoveAdminRightsAsync(string email)
    {
        AccountDto accountDto = new AccountDto {Email = email};
        return await _responseMaker.PutAsync(accountDto, Constants.Routes.MakeUser);
    }
}