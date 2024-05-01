using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace Server.tcpServer
{
    public class PasswordEncryptor
    {
        public static int nIterations = 10101;
        public static int nHash = 70;


        public static string GenerateSalt(int saltSize)
        {
            byte[] saltBytes = new byte[saltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt, int iterations, int hashSize)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, iterations))
            {
                byte[] hashBytes = pbkdf2.GetBytes(hashSize);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }

}
