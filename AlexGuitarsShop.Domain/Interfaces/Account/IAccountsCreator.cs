using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountsCreator
{
    Task<IResult<User>> AddAccountAsync(RegisterViewModel model);
}