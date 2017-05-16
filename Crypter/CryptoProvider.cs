using System.IO;
using System.Security.Cryptography;

namespace Crypter
{
    public static class CryptoProvider
    {
        private static long _memoryFileLimit = 524288000; //500 Mb

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
            if (string.IsNullOrWhiteSpace(password)) return null;
            if (salt != null)
                using (var alg = CreateAlg(password, salt, iterations))
                    return new SymmKeyInfo(alg.Key, alg.IV);
            var keyData = password.GetHashBytes(HashHelper.HashType.Sha256);
            var ivData = password.Reverse().GetHashBytes(HashHelper.HashType.Sha256);
            var key = new SymmKeyInfo(keyData, ivData);
            return key;
        }

        #region basic crypting

        public static string Encrypt(this string value, SymmKeyInfo key = null)
        {
            return EncryptString(value, key).ToBase64();
        }

        private static byte[] EncryptString(string toEncrypt, SymmKeyInfo key = null)
        {
            if (string.IsNullOrWhiteSpace(toEncrypt))
                return null;
            using (var alg = CreateAlg(key))
            using (var encryptor = alg.CreateEncryptor())
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                    swEncrypt.Write(toEncrypt);
                return msEncrypt.ToArray();
            }
        }

        public static string Decrypt(this string value, SymmKeyInfo key = null)
        {
            return string.IsNullOrWhiteSpace(value) ? null : DecryptString(value.FromBase64Bytes(), key);
        }

        private static string DecryptString(byte[] toDecrypt, SymmKeyInfo key = null)
        {
            if (toDecrypt == null || toDecrypt.Length == 0)
                return null;
            using (var alg = CreateAlg(key))
            using (var decryptor = alg.CreateDecryptor())
            using (var msDecrypt = new MemoryStream(toDecrypt))
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (var srDecrypt = new StreamReader(csDecrypt))
                return srDecrypt.ReadToEnd();
        }

        public static void EncryptFile(SymmKeyInfo key, string sourceFilename, string destinationFilename = null)
        {
            if (!File.Exists(sourceFilename))
                throw new FileNotFoundException(sourceFilename);
            var attributes = File.GetAttributes(sourceFilename);
            var creationTime = File.GetCreationTime(sourceFilename);
            var writeTime = File.GetLastWriteTime(sourceFilename);
            var accessTime = File.GetLastAccessTime(sourceFilename);
            var dstFile = string.IsNullOrWhiteSpace(destinationFilename) ? Path.GetTempFileName() : destinationFilename;
            using (var alg = CreateAlg(key))
            using (var encryptor = alg.CreateEncryptor())
            using (var destination = new FileStream(dstFile, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var cryptoStream = new CryptoStream(destination, encryptor, CryptoStreamMode.Write))
            using (var source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                source.CopyTo(cryptoStream);
            File.SetAttributes(dstFile, attributes);
            File.SetCreationTime(dstFile, creationTime);
            File.SetLastAccessTime(dstFile, accessTime);
            File.SetLastWriteTime(dstFile, writeTime);
            if (!string.IsNullOrWhiteSpace(destinationFilename)) return;
            File.Delete(sourceFilename);
            File.Move(dstFile, sourceFilename);
        }

        public static void DecryptFile(SymmKeyInfo key, string sourceFilename, string destinationFilename = null)
        {
            if (!File.Exists(sourceFilename))
                throw new FileNotFoundException(sourceFilename);
            var attributes = File.GetAttributes(sourceFilename);
            var creationTime = File.GetCreationTime(sourceFilename);
            var writeTime = File.GetLastWriteTime(sourceFilename);
            var accessTime = File.GetLastAccessTime(sourceFilename);
            var dstFile = string.IsNullOrWhiteSpace(destinationFilename) ? Path.GetTempFileName() : destinationFilename;
            using (var alg = CreateAlg(key))
            using (var decryptor = alg.CreateDecryptor())
            using (var destination = new FileStream(dstFile, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var cryptoStream = new CryptoStream(destination, decryptor, CryptoStreamMode.Write))
            {
                using (var source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    source.CopyTo(cryptoStream);
            }
            File.SetAttributes(dstFile, attributes);
            File.SetCreationTime(dstFile, creationTime);
            File.SetLastAccessTime(dstFile, accessTime);
            File.SetLastWriteTime(dstFile, writeTime);
            if (!string.IsNullOrWhiteSpace(destinationFilename)) return;
            File.Delete(sourceFilename);
            File.Move(dstFile, sourceFilename);
        }

        public static void EncryptFileMem(SymmKeyInfo key, string sourceFilename, string destinationFilename = null)
        {
            if (!File.Exists(sourceFilename))
                throw new FileNotFoundException(sourceFilename);
            if (new FileInfo(sourceFilename).Length > _memoryFileLimit)
                throw new FileLoadException("File is too big", sourceFilename);

            var attributes = File.GetAttributes(sourceFilename);
            var creationTime = File.GetCreationTime(sourceFilename);
            var writeTime = File.GetLastWriteTime(sourceFilename);
            var accessTime = File.GetLastAccessTime(sourceFilename);
            if (string.IsNullOrWhiteSpace(destinationFilename))
                destinationFilename = sourceFilename;
            using (var alg = CreateAlg(key))
            using (var encryptor = alg.CreateEncryptor())
            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                using (var source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    source.CopyTo(cryptoStream);
                cryptoStream.FlushFinalBlock();
                memoryStream.Position = 0;
                using (var destination = new FileStream(destinationFilename, FileMode.Create, FileAccess.Write, FileShare.None))
                    memoryStream.CopyTo(destination);
            }
            File.SetAttributes(destinationFilename, attributes);
            File.SetCreationTime(destinationFilename, creationTime);
            File.SetLastAccessTime(destinationFilename, accessTime);
            File.SetLastWriteTime(destinationFilename, writeTime);
        }

        public static void DecryptFileMem(SymmKeyInfo key, string sourceFilename, string destinationFilename = null)
        {
            if (!File.Exists(sourceFilename))
                throw new FileNotFoundException(sourceFilename);
            if (new FileInfo(sourceFilename).Length > _memoryFileLimit)
                throw new FileLoadException("File is too big", sourceFilename);

            var attributes = File.GetAttributes(sourceFilename);
            var creationTime = File.GetCreationTime(sourceFilename);
            var writeTime = File.GetLastWriteTime(sourceFilename);
            var accessTime = File.GetLastAccessTime(sourceFilename);
            if (string.IsNullOrWhiteSpace(destinationFilename))
                destinationFilename = sourceFilename;
            using (var alg = CreateAlg(key))
            using (var decryptor = alg.CreateDecryptor())
            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
            {
                using (var source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    source.CopyTo(cryptoStream);
                cryptoStream.FlushFinalBlock();
                memoryStream.Position = 0;
                using (var destination = new FileStream(destinationFilename, FileMode.Create, FileAccess.Write, FileShare.None))
                    memoryStream.CopyTo(destination);
            }
            File.SetAttributes(destinationFilename, attributes);
            File.SetCreationTime(destinationFilename, creationTime);
            File.SetLastAccessTime(destinationFilename, accessTime);
            File.SetLastWriteTime(destinationFilename, writeTime);
        }

        #endregion basic crypting
    }
}