using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Bhusamadhan.DB
{
    public class QueryStringHelper
    {
        private static readonly string Key = "Bhusamadhan2026Key";
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            byte[] clearBytes = Encoding.UTF8.GetBytes(plainText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = GetKey();
                aes.GenerateIV();

                using (MemoryStream ms = new MemoryStream())
                {
                    // Store IV along with encrypted data
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (CryptoStream cs = new CryptoStream(  ms,  aes.CreateEncryptor(),  CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.FlushFinalBlock();
                    }

                    return HttpServerUtility.UrlTokenEncode(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;

            try
            {
                byte[] cipherBytes =  HttpServerUtility.UrlTokenDecode(cipherText);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = GetKey();

                  
                    byte[] iv = new byte[16];
                    Array.Copy(cipherBytes, 0, iv, 0, 16);

                    aes.IV = iv;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream( ms, aes.CreateDecryptor(),  CryptoStreamMode.Write))
                        {
                            cs.Write( cipherBytes, 16, cipherBytes.Length - 16);

                            cs.FlushFinalBlock();
                        }

                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        private static byte[] GetKey()
        {
            using (SHA256 sha = SHA256.Create())
            {
                return sha.ComputeHash( Encoding.UTF8.GetBytes(Key));
            }
        }

    }
}