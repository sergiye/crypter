using System;
using System.IO;
using System.Security.Cryptography;

namespace XtzCrypter
{
    public static class AesCryptoProvider
    {
        private static SymmetricAlgorithm CreateAlg(SymmKeyInfo key = null)
        {
            var alg = new RijndaelManaged
                      {
                          Mode = CipherMode.CBC,
                          Padding = PaddingMode.PKCS7,
                      };
            alg.BlockSize = alg.LegalBlockSizes[0].MaxSize;
            alg.KeySize = alg.LegalKeySizes[0].MaxSize;
            if (key != null)
            {
                if (key.Key != null) alg.Key = key.Key;
                if (key.Iv != null) alg.IV = key.Iv;
            }
            else
            {
                //instantiating the AESManaged Class already generates the keys for you, so this code could be removed
//                alg.GenerateKey();
//                alg.GenerateIV();
                alg.Key = "Gqx7Asyzjb5jTOrPgTnjkM3VoHaxtinNIHJ3O11vjg8=".FromBase64Bytes();
                alg.IV = "fuswGv+u5TgJ2OtfEr6JPK1l3nXoiLpCrQEbBKEsZo8=".FromBase64Bytes();
            }
            return alg;
        }

        private static SymmetricAlgorithm CreateAlg(string password, byte[] salt, int iterations)
        {
            var aes = new RijndaelManaged
                      {
                          Mode = CipherMode.CBC,
                          Padding = PaddingMode.PKCS7
                      };
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            var key = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            return aes;
        }

        public static SymmKeyInfo GetKey(string password, byte[] salt = null, int iterations = 1)
        {
            if (salt != null)
                using (var alg = CreateAlg(password, salt, iterations))
                    return new SymmKeyInfo(alg.Key, alg.IV);
            if (string.IsNullOrWhiteSpace(password)) return null;
            var keyData = password.GetHashBytes(CryptingHelper.HashType.Sha256);
            var ivData = password.Reverse().GetHashBytes(CryptingHelper.HashType.Sha256);
            var key = new SymmKeyInfo(keyData, ivData);
            return key;
        }

        public static byte[] Encrypt(string toEncrypt, SymmKeyInfo key)
        {
            if (String.IsNullOrWhiteSpace(toEncrypt))
                return null;
            using (var alg = CreateAlg(key))
            using (var encryptor = alg.CreateEncryptor(alg.Key, alg.IV))
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                    swEncrypt.Write(toEncrypt);
                return msEncrypt.ToArray();
            }
        }

        public static string Decrypt(byte[] toDecrypt, SymmKeyInfo key)
        {
            if (toDecrypt == null || toDecrypt.Length == 0)
                return null;
            using (var alg = CreateAlg(key))
            using (var decryptor = alg.CreateDecryptor(alg.Key, alg.IV))
            using (var msDecrypt = new MemoryStream(toDecrypt))
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (var srDecrypt = new StreamReader(csDecrypt))
                return srDecrypt.ReadToEnd();
        }

        public static void EncryptFile(SymmKeyInfo key, string sourceFilename, string destinationFilename = null)
        {
            if (!File.Exists(sourceFilename))
                throw new FileNotFoundException(sourceFilename);
            var dstFile = string.IsNullOrWhiteSpace(destinationFilename) ? Path.GetTempFileName() : destinationFilename;
            using (var alg = CreateAlg(key))
            using (var encryptor = alg.CreateEncryptor(alg.Key, alg.IV))
            using (var destination = new FileStream(dstFile, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var cryptoStream = new CryptoStream(destination, encryptor, CryptoStreamMode.Write))
            using (var source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                source.CopyTo(cryptoStream);
            if (!string.IsNullOrWhiteSpace(destinationFilename)) return;
            File.Delete(sourceFilename);
            File.Move(dstFile, sourceFilename);
        }

        public static void DecryptFile(SymmKeyInfo key, string sourceFilename, string destinationFilename = null)
        {
            if (!File.Exists(sourceFilename))
                throw new FileNotFoundException(sourceFilename);
            var dstFile = string.IsNullOrWhiteSpace(destinationFilename) ? Path.GetTempFileName() : destinationFilename;
            using (var alg = CreateAlg(key))
            using (var decryptor = alg.CreateDecryptor(alg.Key, alg.IV))
            using (var destination = new FileStream(dstFile, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var cryptoStream = new CryptoStream(destination, decryptor, CryptoStreamMode.Write))
            {
                using (var source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    source.CopyTo(cryptoStream);
            }
            if (!string.IsNullOrWhiteSpace(destinationFilename)) return;
            File.Delete(sourceFilename);
            File.Move(dstFile, sourceFilename);
        }
    }
}