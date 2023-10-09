using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountsCreator
{
    Task<IResult<AccountDto>> AddAccountAsync(AccountDto accountDto);
}