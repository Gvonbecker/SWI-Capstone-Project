using Microsoft.AspNetCore.Identity;
using SWI_Form_Client.Models;
using System.Security.Cryptography;

namespace SWI_Form_Client.Utility
{
    public static class EncryptionHelper
    {
        /// <summary>
        /// Takes in a Login user and a password and returns the hashed password.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string HashPassword(Login login, string pass)
        {
            PasswordHasher<Login> hasher = new PasswordHasher<Login>();
            return hasher.HashPassword(login, pass);
        }

        /// <summary>
        /// Takes in a Login user, a hashed password, and the sent password. Returns bool.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="hash"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static bool VerifyPassword(Login login, string hash, string pass)
        {
            PasswordHasher<Login> hasher = new PasswordHasher<Login>();

            int result = (int) hasher.VerifyHashedPassword(login, hash, pass);

            if (result == 1 || result == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
