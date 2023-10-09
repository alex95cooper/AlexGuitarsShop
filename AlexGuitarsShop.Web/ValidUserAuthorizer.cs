using System.Security.Claims;
using AlexGuitarsShop.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AlexGuitarsShop.Web;

public class ValidUserAuthorizer : IAuthorizer
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ValidUserAuthorizer(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;

    public async Task SignIn(AccountDto accountDto)
    {
        ClaimsIdentity claimsIdentity = Authenticate(accountDto);
        await Context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
    }

    public async Task SignOut()
    {
        await Context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private static ClaimsIdentity Authenticate(AccountDto accountDto)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, accountDto.Email),
            new(ClaimsIdentity.DefaultRoleClaimType, accountDto.Role.ToString())
        };

        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}