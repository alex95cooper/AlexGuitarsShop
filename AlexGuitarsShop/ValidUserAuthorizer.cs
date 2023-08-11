using System.Security.Claims;
using AlexGuitarsShop.DAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AlexGuitarsShop;

public class ValidUserAuthorizer
{
    private readonly HttpContext _context;

    public ValidUserAuthorizer(HttpContext context)
    {
        _context = context;
    }

    public async Task SignIn(User user)
    {
        ClaimsIdentity claimsIdentity = Authenticate(user);
        if (_context == null) throw new ArgumentNullException(nameof(_context));
        await _context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity!));
    }

    public async Task SignOut()
    {
        if (_context == null) throw new ArgumentNullException(nameof(_context));
        await _context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private static ClaimsIdentity Authenticate(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (user.Email == null)
            throw new ArgumentNullException(nameof(user.Email));

        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}