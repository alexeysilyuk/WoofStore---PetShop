using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class HashPass
    {
        public static string GenerateHash(string password)
        {
            System.Security.Cryptography.SHA256 sha = System.Security.Cryptography.SHA256.Create();
            string hashed = System.Convert.ToBase64String(System.Text.UnicodeEncoding.Unicode.GetBytes(password));
            return hashed.Length > 49 ? hashed.Substring(0, 49) : hashed;

        }

    }
}