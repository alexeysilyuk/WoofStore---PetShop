using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace mvc_project.Models
{
    public class HashPass
    {

        public const int SALT_SIZE = 24;
        public const int HASH_SIZE = 24;
        public const int PBK = 500;

        public static string GenerateHash(string password)
        {
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_SIZE];
            csprng.GetBytes(salt);
            byte[] hash = HashPass.PBKDF2(password, salt, PBK, HASH_SIZE);
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);


            //System.Security.Cryptography.SHA256 sha = System.Security.Cryptography.SHA256.Create();
            //string hashed = System.Convert.ToBase64String(System.Text.UnicodeEncoding.Unicode.GetBytes(password));
            //return hashed.Length > 49 ? hashed.Substring(0, 49) : hashed;

        }

        private static byte[] PBKDF2(string password, byte[] salt, int pBK, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = pBK;
            return pbkdf2.GetBytes(outputBytes); 
        }

        public static bool Equals (byte[] dbHash, byte[] passHash)
        {
            uint diff = (uint)dbHash.Length ^ (uint)passHash.Length;
            for (int i=0; i<dbHash.Length && i<passHash.Length; i++)
            {
                diff |= (uint)dbHash[i] ^ (uint)passHash[i];
            }
            return diff == 0;
        }

        public static bool ValidatePassword(string password, string dbHash)
        {
            char[] delimeter = { ':' };
            string[] split = dbHash.Split(delimeter);
            byte[] salt = Convert.FromBase64String(split[0]);
            byte[] hash = Convert.FromBase64String(split[1]);
            byte[] hashToValidate = HashPass.PBKDF2(password, salt, PBK, hash.Length);
            return HashPass.Equals(hash, hashToValidate);

        }

    }
}