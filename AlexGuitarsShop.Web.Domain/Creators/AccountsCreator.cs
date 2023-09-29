using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Extensions;
using AlexGuitarsShop.Web.Domain.Interfaces.Account;
using AlexGuitarsShop.Web.Domain.ViewModels;

namespace AlexGuitarsShop.Web.Domain.Creators;

public class AccountsCreator : IAccountsCreator
{
    private readonly IResponseMaker _responseMaker;

    public AccountsCreator(IResponseMaker responseMaker)
    {
        _responseMaker = responseMaker;
    }

    public async Task<IResult<AccountDto>> AddAccountAsync(RegisterViewModel model)
    {
        if (model == null)
        {
            ResultCreator.GetInvalidResult<AccountDto>(
                Constants.Account.IncorrectAccount, HttpStatusCode.BadRequest);
        }
        
        AccountDto registerDto = model.ToAccountDto();
        return await _responseMaker.PostAsync(registerDto, Constants.Routes.Register);
    }
}