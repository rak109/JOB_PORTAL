using System.Security.Cryptography;
using System.Text;

namespace JOB_PORTAL
{
    public class PasswordHelper
    {

        public static byte[] Hash(string password)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static bool Verify(string enteredPassword, byte[] storedHash)
        {
            var enteredHash = Hash(enteredPassword);
            if (enteredHash.Length != storedHash.Length) return false;

            for (int i = 0; i < enteredHash.Length; i++)
            {
                if (enteredHash[i] != storedHash[i])
                    return false;
            }
            return true;
        }

    }
}

