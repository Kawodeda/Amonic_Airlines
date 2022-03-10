using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AMONIC_Desktop
{
    public class PasswordConverter
    {
        public static string Encrypt(string password)
        {
            string hex = "";

            using(MD5 md5 = MD5.Create())
            {
                byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
                byte[] hash = md5.ComputeHash(passwordBytes);

                StringBuilder builder = new StringBuilder();
                foreach(byte symbol in hash)
                {
                    builder.Append(symbol.ToString("X2"));
                }

                hex = builder.ToString();
            }

            return hex;
        }
    }
}
