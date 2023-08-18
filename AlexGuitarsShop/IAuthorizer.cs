using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop;

public interface IAuthorizer
{
    Task SignIn(Account account);

    Task SignOut();
}