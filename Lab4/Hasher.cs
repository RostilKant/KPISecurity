using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Lab4
{
    public static class Hasher
    {
        public static void StoreHashes(List<string> passwords)
        {
           
            SavePasswordsHashes(passwords, "../../../MD5.csv", HashWithMd5);
            SavePasswordsHashes(passwords, "../../../SHA1.csv", HashWithSha1);
            SavePasswordsHashes(passwords, "../../../BCrypt.csv", HashWithBCrypt);
        }
        private static void SavePasswordsHashes(IEnumerable<string> passwords, string filePath, Func<string, string> hashingFunction)
        {
            var sw = new StreamWriter(filePath);

            foreach (var password in passwords)
            {
                sw.WriteLine(hashingFunction(password));
            }

            sw.Close();
        }

        private static string HashWithMd5(string password) => 
            Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
           
        
        private static string HashWithSha1(string password)
        {
            var sha1 = SHA1.Create();

            var salt = new byte[16];
            var random = RandomNumberGenerator.Create();
            random.GetBytes(salt);
            var stringSalt = Convert.ToBase64String(salt);

            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password + stringSalt));

            return "Hash: " + Convert.ToBase64String(hash) + " Salt: " + stringSalt;
        }

        private static string HashWithBCrypt(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(5);
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
    }
}