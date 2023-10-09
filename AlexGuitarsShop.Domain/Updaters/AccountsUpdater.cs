using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;

namespace AlexGuitarsShop.Domain.Updaters;

public class AccountsUpdater : IAccountsUpdater
{
    private readonly IAccountRepository _accountRepository;

    public AccountsUpdater(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IResult<AccountDto>> SetAdminRightsAsync(string email)
    {
        await _accountRepository.UpdateAsync(email, (int) Role.Admin);
        return ResultCreator.GetValidResult(
            new AccountDto {Email = email}, HttpStatusCode.OK);
    }

    public async Task<IResult<AccountDto>> RemoveAdminRightsAsync(string email)
    {
        await _accountRepository.UpdateAsync(email, (int) Role.User);
        return ResultCreator.GetValidResult(
            new AccountDto {Email = email}, HttpStatusCode.OK);
    }
}