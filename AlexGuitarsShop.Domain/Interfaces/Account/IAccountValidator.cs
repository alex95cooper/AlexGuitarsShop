using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountValidator
{
    Task<bool> CheckIfEmailExist(string email);
    
    bool CheckIfRegisterIsValid(Register register);

    bool CheckIfLoginIsValid(Login login);
}