using System.Security.Cryptography;
using System.Text;

namespace AlexGuitarsShop.Domain;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        if (password == null) throw new ArgumentNullException(nameof(password));
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        return hash;
    }
}