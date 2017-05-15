using System.IO;
using System.Security.Cryptography;

namespace XtzCrypter
{
    public static class AesCryptoProvider
    {
        private static readonly SymmKeyInfo DefaultKey = new SymmKeyInfo("/twqoenf4qyRKbHgHgNq+7c2PcOfCzGwuN3u3YyajQA="
            , "A9kO6HT2tyKkiuBjhqFFZA==");

        private static SymmetricAlgorithm CreateAlg(SymmKeyInfo key = null, bool generateKey = false)
        {
            var alg = new RijndaelManaged
                      {
                          Mode = CipherMode.CBC,
                          Padding = PaddingMode.PKCS7,
                          KeySize = 256,
                          BlockSize = 128
                      };
            if (generateKey)
            {
                //todo: instantiating the AESManaged Class already generates the keys for you, so this code could be removed
                alg.GenerateKey();
                alg.GenerateIV();
            }
            else
            {
                alg.Key =
                    (key != null && !string.IsNullOrWhiteSpace(key.Key) ? key.Key : DefaultKey.Key).FromBase64Bytes();
                alg.IV = (key != null && !string.IsNullOrWhiteSpace(key.Iv) ? key.Iv : DefaultKey.Iv).FromBase64Bytes();
            }
            return alg;
        }

        private static SymmetricAlgorithm CreateAlg(string password, byte[] salt, int iterations)
        {
            var aes = new RijndaelManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            // NB: Rfc2898DeriveBytes initialization and subsequent calls to   GetBytes   must be eactly the same, including order, on both the encryption and decryption sides.
            var key = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;
            return aes;
        }

        public static SymmKeyInfo GetNewKey()
        {
            using (var alg = CreateAlg(null, true))
                return new SymmKeyInfo(alg.Key.ToBase64(), alg.IV.ToBase64());
        }

        public static SymmKeyInfo GenerateNewKey(string password, byte[] salt, int iterations)
        {
            using (var alg = CreateAlg(password, salt, iterations))
                return new SymmKeyInfo(alg.Key.ToBase64(), alg.IV.ToBase64());
        }

        public static byte[] Encrypt(string toEncrypt, SymmKeyInfo key)
        {
            if (string.IsNullOrWhiteSpace(toEncrypt))
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

        public static void EncryptFile(SymmKeyInfo key, string sourceFilename, string destinationFilename)
        {
            if (!File.Exists(sourceFilename))
                throw new FileNotFoundException(sourceFilename);
            using (var alg = CreateAlg(key))
            using (var encryptor = alg.CreateEncryptor(alg.Key, alg.IV))
            using (var destination = new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            using (var cryptoStream = new CryptoStream(destination, encryptor, CryptoStreamMode.Write))
            using (var source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                source.CopyTo(cryptoStream);
        }

        public static void DecryptFile(SymmKeyInfo key, string sourceFilename, string destinationFilename)
        {
            if (!File.Exists(sourceFilename))
                throw new FileNotFoundException(sourceFilename);
            using (var alg = CreateAlg(key))
            using (var decryptor = alg.CreateDecryptor(alg.Key, alg.IV))
            using (var destination = new FileStream(destinationFilename, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var cryptoStream = new CryptoStream(destination, decryptor, CryptoStreamMode.Write))
            {
                using (var source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    source.CopyTo(cryptoStream);
            }
        }
    }
}