using System.Net;
using System.Text.RegularExpressions;
using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;

namespace AlexGuitarsShop.Domain.Validators;

public class AccountValidator : IAccountValidator
{
    private readonly IAccountRepository _accountRepository;

    public AccountValidator(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IResult<AccountDto>> CheckIfEmailExist(string email)
    {
        if (email == null)
        {
            ResultCreator.GetInvalidResult<AccountDto>(
                Constants.ErrorMessages.InvalidEmail, HttpStatusCode.BadRequest);
        }

        return await _accountRepository.FindAsync(email) != null
            ? ResultCreator.GetValidResult(new AccountDto {Email = email}, HttpStatusCode.OK)
            : ResultCreator.GetInvalidResult<AccountDto>(
                Constants.ErrorMessages.InvalidEmail, HttpStatusCode.BadRequest);
    }

    public IResult<AccountDto> CheckIfRegisterIsValid(AccountDto accountDto)
    {
        if (accountDto != null
            && CheckIfNameIsValid(accountDto.Name)
            && CheckIfEmailIsValid(accountDto.Email)
            && CheckIfPasswordIsValid(accountDto.Password))
        {
            return ResultCreator.GetValidResult(accountDto, HttpStatusCode.OK);
        }

        return ResultCreator.GetInvalidResult<AccountDto>(
            Constants.ErrorMessages.InvalidAccount, HttpStatusCode.BadRequest);
    }

    public IResult<AccountDto> CheckIfLoginIsValid(AccountDto accountDto)
    {
        if (accountDto != null
            && CheckIfEmailIsValid(accountDto.Email)
            && CheckIfPasswordIsValid(accountDto.Password))
        {
            return ResultCreator.GetValidResult(accountDto, HttpStatusCode.OK);
        }

        return ResultCreator.GetInvalidResult<AccountDto>(
            Constants.ErrorMessages.InvalidAccount, HttpStatusCode.BadRequest);
    }

    private static bool CheckIfNameIsValid(string name)
    {
        if (name == null)
        {
            return false;
        }

        return name.Length is >= Constants.Account.NameMinLength
            and <= Constants.Account.NameMaxLength;
    }

    private static bool CheckIfEmailIsValid(string email)
    {
        if (email == null)
        {
            return false;
        }

        Regex regex = new(Constants.Account.EmailPattern);
        return regex.IsMatch(email);
    }

    private static bool CheckIfPasswordIsValid(string password)
    {
        if (password == null)
        {
            return false;
        }

        return password.Length is >= Constants.Account.PasswordMinLength
            and <= Constants.Account.PasswordMaxLength;
    }
}