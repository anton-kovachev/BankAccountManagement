using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankUserAccountManagmentApplicationDAL.Helpers
{
    public static class HashHelper
    {
        public static string GetHashedString(string textForHashing)
        {
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(textForHashing));
                // Get the hashed string.  
                string hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                // Print the string.   
                return hash;
            }
        }
    }
}
