using System.Security.Cryptography;
using System.Text;

namespace Authorization.Application.AppServices.Helpers;

public class PasswordHelper
{
    public static string HashPassword(string stringToEncrypt)
    {
        var buffer = Encoding.UTF8.GetBytes(stringToEncrypt);
        HashAlgorithm sha = SHA256.Create();
        byte[] hash = sha.ComputeHash(buffer);

        return Convert.ToBase64String(hash);
    }
}