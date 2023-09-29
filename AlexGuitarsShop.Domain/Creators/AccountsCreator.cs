using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.Account;

namespace AlexGuitarsShop.Domain.Creators;

public class AccountsCreator : IAccountsCreator
{
    private const string ExistEmailErrorMessage = "User with this e-mail already exists";

    private readonly IAccountRepository _accountRepository;

    public AccountsCreator(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IResult<AccountDto>> AddAccountAsync(AccountDto accountDto)
    {
        var account = await _accountRepository.FindAsync(accountDto.Email);
        if (account != null)
        {
            return ResultCreator.GetInvalidResult<AccountDto>(
                ExistEmailErrorMessage, HttpStatusCode.BadRequest);
        }

        account = accountDto.ToAccount();
        account.Password = PasswordHasher.HashPassword(account.Password);
        await _accountRepository.CreateAsync(account);
        return ResultCreator.GetValidResult(accountDto, HttpStatusCode.OK);
    }
}