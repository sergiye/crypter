using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace XtzCrypter
{
    public static class CryptingHelper
    {
        #region Hash

        public enum HashType
        {
            Md5,  //24 bytes hash
            Sha1, //30 bytes hash
            Sha256 //48 bytes hash
        }

        private static HashAlgorithm GetHashAlgorithm(HashType hashType)
        {
            switch (hashType)
            {
                case HashType.Sha1:
                    return new SHA1Managed();
                case HashType.Sha256:
                    return new SHA256Managed();
                //case HashType.Md5:
                default:
                    return MD5.Create();
            }
        }

        public static string GetHash(this string input, HashType hashType = HashType.Md5)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;
            using (var alg = GetHashAlgorithm(hashType))
            {
                return BitConverter.ToString(alg.ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-", "").ToLower();
            }
        }

        public static string GetHash(this byte[] input, HashType hashType = HashType.Md5)
        {
            if (input == null || input.Length == 0)
                return null;
            using (var alg = GetHashAlgorithm(hashType))
                return BitConverter.ToString(alg.ComputeHash(input)).Replace("-", "").ToLower();
        }

        public static string GetFileHash(string filename, HashType hashType = HashType.Md5)
        {
            if (!File.Exists(filename))
                return null;
            using (var alg = GetHashAlgorithm(hashType))
            using (var stream = File.OpenRead(filename))
                return BitConverter.ToString(alg.ComputeHash(stream)).Replace("-", "").ToLower();
        }

        #endregion Hash

        #region basic crypting

        public static byte[] EncryptString(string clearText, SymmKeyInfo key = null)
        {
            return AesCryptoProvider.Encrypt(clearText, key);
        }

        public static string DecryptString(byte[] encrypted, SymmKeyInfo key = null)
        {
            return AesCryptoProvider.Decrypt(encrypted, key);
        }

        public static string Encrypt(this string value, SymmKeyInfo key = null)
        {
            return EncryptString(value, key).ToBase64();
        }

        public static string Decrypt(this string value, SymmKeyInfo key = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            return DecryptString(value.FromBase64Bytes(), key);
        }

        #endregion basic crypting
    }
}