using System.Security.Cryptography;
using System.Text;

namespace API.Extensions
{
    public static class AuthMethodExtension
    {
        public static bool DecryptPassword(string password, byte[] passwordSalt, byte[] userPassword)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computeHash.Length; i++)
                if (computeHash[i] != userPassword[i])
                    return false;

            return true;
        }
    }
}