using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace authJWT.Common
{
    public static class CryptoService
    {
        private const string AesIv256 = @"1BI0OJOHBTV$#&JM";
        private const string AesKey256 = @"5TMG0OH&)(EC)MIBHC8Z(V&LGV3CR&WC";


        public static string Encrypt(string text)
        {
            var aes = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                IV = Encoding.UTF8.GetBytes(AesIv256),
                Key = Encoding.UTF8.GetBytes(AesKey256),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            // Convert string to byte array
            var src = Encoding.Unicode.GetBytes(text);

            // encryption
            using (var encrypt = aes.CreateEncryptor())
            {
                var dest = encrypt.TransformFinalBlock(src, 0, src.Length);

                // Convert byte array to Base64 strings
                return Convert.ToBase64String(dest);
            }
        }

        public static string Decrypt(string text)
        {
            var aes = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                IV = Encoding.UTF8.GetBytes(AesIv256),
                Key = Encoding.UTF8.GetBytes(AesKey256),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            var src = Convert.FromBase64String(text);

            // decryption
            using (var decrypt = aes.CreateDecryptor())
            {
                var dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                return Encoding.Unicode.GetString(dest);
            }
        }
    }
}
