using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountValidator
{
    Task<IResult> CheckIfEmailExist(string email);

    IResult<AccountDto> CheckIfRegisterIsValid(AccountDto accountDto);

    IResult<AccountDto> CheckIfLoginIsValid(AccountDto accountDto);
}