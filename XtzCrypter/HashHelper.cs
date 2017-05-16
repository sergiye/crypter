using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace XtzCrypter
{
    public static class HashHelper
    {
        public enum HashType
        {
            Md5,  //32 bytes hash
            Sha1, //40 bytes hash
            Sha256 //64 bytes hash
        }

        private static HashAlgorithm GetHashAlgorithm(HashType hashType)
        {
            switch (hashType)
            {
                case HashType.Sha1:
//                    return new SHA1Cng();
                    return new SHA1Managed();
                case HashType.Sha256:
//                    return new SHA256Cng();
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
                return alg.ComputeHash(Encoding.UTF8.GetBytes(input)).ToBase64();
            }
        }

        public static byte[] GetHashBytes(this string input, HashType hashType = HashType.Md5)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;
            using (var alg = GetHashAlgorithm(hashType))
            {
                return alg.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
        }

        public static string GetHash(this byte[] input, HashType hashType = HashType.Md5)
        {
            if (input == null || input.Length == 0)
                return null;
            using (var alg = GetHashAlgorithm(hashType))
                return alg.ComputeHash(input).ToBase64();
        }

        public static string GetFileHash(string filename, HashType hashType = HashType.Md5)
        {
            if (!File.Exists(filename))
                return null;
            using (var alg = GetHashAlgorithm(hashType))
            using (var stream = File.OpenRead(filename))
                return alg.ComputeHash(stream).ToBase64();
        }
    }
}