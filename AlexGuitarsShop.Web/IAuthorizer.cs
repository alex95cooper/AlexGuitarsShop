using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Web;

public interface IAuthorizer
{
    Task SignIn(Account account);

    Task SignOut();
}