using AlexGuitarsShop.Domain.Models;

namespace AlexGuitarsShop.Domain.Interfaces.Account;

public interface IAccountsCreator
{
    Task<IResult<DAL.Models.Account>> AddAccountAsync(Register register);
}