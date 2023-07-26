using System.Security.Claims;
using AlexGuitarsShop.DAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AlexGuitarsShop;

public static class ValidUserAuthorizer
{
    public static async Task SignIn(HttpContext context, User user)
    {
        ClaimsIdentity claimsIdentity = Authenticate(user);
        await context!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity!));
    }

    public static async Task SignOut(HttpContext context)
    {
        await context!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private static ClaimsIdentity Authenticate(User user)
    {
        if (user == null) return new ClaimsIdentity();
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Email!),
            new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}