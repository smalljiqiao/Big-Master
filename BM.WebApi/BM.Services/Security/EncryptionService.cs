using System;
using System.Security.Cryptography;
using System.Text;

namespace BM.Services.Security
{
    public static class EncryptionService
    {
        public static string CreateSaltKey(int size)
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                var buff = new byte[size];
                provider.GetBytes(buff);

                return Convert.ToBase64String(buff);
            }
        }

        public static string CreatePasswordHash(string password, string saltKey, string passwordFormat = "SHA1")
        {
            return CreateHash(Encoding.UTF8.GetBytes(string.Concat(password, saltKey)), passwordFormat);
        }

        public static string CreateHash(byte[] data, string hashAlgorithm = "SHA1")
        {
            if (string.IsNullOrEmpty(hashAlgorithm))
                hashAlgorithm = "SHA1";

            var algorithm = HashAlgorithm.Create(hashAlgorithm);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name");

            var hashByteArray = algorithm.ComputeHash(data);
            return BitConverter.ToString(hashByteArray).Replace("-", string.Empty);
        }
    }
}
