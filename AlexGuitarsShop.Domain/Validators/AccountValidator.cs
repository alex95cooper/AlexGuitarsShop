using System.Text.RegularExpressions;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Models;

namespace AlexGuitarsShop.Domain.Validators;

public class AccountValidator : IAccountValidator
{
    private const string EmailPattern = @"^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$";

    private readonly IAccountRepository _accountRepository;

    public AccountValidator(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<bool> CheckIfEmailExist(string email)
    {
        return await _accountRepository.FindAsync(email) != null;
    }

    public async Task<bool> CheckIfAdminsPageIsValid(int pageNumber, int limit)
    {
        int count = await _accountRepository.GetAdminsCountAsync();
        return CheckIfPageIsValid(count, pageNumber, limit);
    }

    public async Task<bool> CheckIfUsersPageIsValid(int pageNumber, int limit)
    {
        int count = await _accountRepository.GetUsersCountAsync();
        return CheckIfPageIsValid(count, pageNumber, limit);
    }

    public bool CheckIfRegisterIsValid(Register register)
    {
        register = register ?? throw new ArgumentNullException(nameof(register));
        return CheckIfNameIsValid(register.Name)
               && CheckIfEmailIsValid(register.Email)
               && CheckIfPasswordIsValid(register.Password);
    }

    public bool CheckIfLoginIsValid(Login login)
    {
        login = login ?? throw new ArgumentNullException(nameof(login));
        return CheckIfEmailIsValid(login.Email)
               && CheckIfPasswordIsValid(login.Password);
    }

    private static bool CheckIfPageIsValid(int count, int pageNumber, int limit)
    {
        if (count < limit)
        {
            return pageNumber == 1;
        }

        int pageCount = count / limit;
        pageCount = count % limit > 0 ? pageCount + 1 : pageCount;
        return pageNumber > 0 && pageNumber <= pageCount;
    }

    private static bool CheckIfNameIsValid(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        return name.Length is > 3 and < 20;
    }

    private static bool CheckIfEmailIsValid(string email)
    {
        if (email == null)
        {
            throw new ArgumentNullException(nameof(email));
        }

        Regex regex = new(EmailPattern);
        return regex.IsMatch(email);
    }

    private static bool CheckIfPasswordIsValid(string password)
    {
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password));
        }

        return password.Length is > 5 and < 50;
    }
}