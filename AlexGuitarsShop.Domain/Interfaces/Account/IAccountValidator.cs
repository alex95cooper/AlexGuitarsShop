using AlexGuitarsShop.Domain.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountValidator
{
    Task<bool> CheckIfEmailExist(string email);

    Task<bool> CheckIfAdminsPageIsValid(int pageNumber, int limit);

    Task<bool> CheckIfUsersPageIsValid(int pageNumber, int limit);

    bool CheckIfRegisterIsValid(Register register);

    bool CheckIfLoginIsValid(Login login);
}