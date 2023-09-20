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

    public async Task<bool> CheckIfEmailExist(string email)
    {
        if (email == null)
        {
            return false;
        }

        return await _accountRepository.FindAsync(email) != null;
    }

    public bool CheckIfRegisterIsValid(Register register)
    {
        return register != null
               && CheckIfNameIsValid(register.Name)
               && CheckIfEmailIsValid(register.Email)
               && CheckIfPasswordIsValid(register.Password);
    }

    public bool CheckIfLoginIsValid(Login login)
    {
        return login != null
               && CheckIfEmailIsValid(login.Email)
               && CheckIfPasswordIsValid(login.Password);
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