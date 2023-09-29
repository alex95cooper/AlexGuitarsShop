using AlexGuitarsShop.Common.Models;

namespace AlexGuitarsShop.Web;

public interface IAuthorizer
{
    Task SignIn(AccountDto accountDto);

    Task SignOut();
}