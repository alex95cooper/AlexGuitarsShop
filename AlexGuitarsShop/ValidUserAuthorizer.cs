using System.Security.Claims;
using AlexGuitarsShop.DAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AlexGuitarsShop;

public class ValidUserAuthorizer : IAuthorizer
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ValidUserAuthorizer(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;

    public async Task SignIn(Account account)
    {
        ClaimsIdentity claimsIdentity = Authenticate(account);
        await Context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
    }

    public async Task SignOut()
    {
        await Context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private static ClaimsIdentity Authenticate(Account account)
    {
        account = account ?? throw new ArgumentNullException(nameof(account));
        if (account.Email == null)
        {
            throw new ArgumentNullException(nameof(account.Email));
        }

        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, account.Email),
            new(ClaimsIdentity.DefaultRoleClaimType, account.Role.ToString())
        };

        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}