using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountsCreator
{
    Task<IResponse<User>> AddAccountAsync(RegisterViewModel model);
}