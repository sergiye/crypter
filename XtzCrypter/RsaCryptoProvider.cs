using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace XtzCrypter
{
    public class RsaCryptoProvider
    {
        private const bool UseOaep = false;
        private static readonly byte[] DefaultExponentHex = {1,0,1};
        private readonly RSACryptoServiceProvider _sp;
        public const int AsymmKeyLength = 1024;

        public RsaCryptoProvider()
        {
            var cspParams = new CspParameters
            {
                Flags = CspProviderFlags.CreateEphemeralKey
                //Flags = CspProviderFlags.UseMachineKeyStore
            };
            _sp = new RSACryptoServiceProvider(AsymmKeyLength, cspParams) {PersistKeyInCsp = false};
            _sp.FromXmlString("<RSAKeyValue><Modulus>nd9nocS2PnFEBxXFuI33G9dwAbIGhz1W5478KkpEf/ULcdMoV4FWvmp8fHsU3lBjs7Dy//dllmryASy2aO1h4/I3Ggyr24Wjetks0cndXpQjwW+QwO1KV/l/GNMFIXHMnSEkymSYL7iUuy7ycmqllmfud7FESRTnF/INeS/3ET8=</Modulus><Exponent>AQAB</Exponent><P>zJ5VMh01JHzkV5D8mKeH6avmUu+ZoVDoojCTOZq5Gu5ukyxCe3obuN6fnLe7YUA1s+MKClO/1ZLVfSEBP1SxEQ==</P><Q>xYQSDfCrZ4GGyDVcNOAn07wJQibNwNLyuZQJRyFVh3+8CyV3MXPHww+X/HyoVYDnsvfz9pr3+NwVHA4rUG2dTw==</Q><DP>qBX4zm4H1a1ytiw4E/6rO10mm0KP5WBdeb6FcnCTVng/BU76Xgx08WyPmWGk38KrWtZKzSj3ES7JiTyvlaKwwQ==</DP><DQ>Q0e9rqnweQ2SD9i9U/WXG3TN4o69P5WbwMNAdc5RCBrmvxVACMRbo4JIT2VXIekLA9eabsJS/Z5aQnhBIOB12Q==</DQ><InverseQ>OlJIfmJYfIGMLlkW8yDwQ5i+ejGupS9j2JXnuJoWGhaZmtXM16qxrncEoRaHUyMB6VMOjoqTaHBR4h0aAAic+Q==</InverseQ><D>Cs91IYmpJRDNJLhj8qWd/y+7Fr1MXyRfIroSywk3iZThChP4gOLKwCfl4Fbumzk2qo5PsCFjuRoXpObykrAhR3Fm2XDTfI/DCBbyTQ89BXI6KD46qeOyaKQa/0+vJfTsixDUR80b1zPr/w9Y6q7SJ/W1PLGgxQg7SyJOY+wwe2E=</D></RSAKeyValue>");
        }

        #region IAsymmCryptoProvider Members

        private byte[] _asymmPublicKey;
        public byte[] AsymmPublicKey
        {
            get
            {
                if (_asymmPublicKey == null)
                {
                    //return _sp.ToXmlString(false);
                    var parameters = _sp.ExportParameters(false);
                    _asymmPublicKey = parameters.Modulus;
                }
                return _asymmPublicKey;
            }
        }

        public string Decrypt(string toDecrypt)
        {
            var data = toDecrypt.FromBase64Bytes();
            var decryptedData = _sp.Decrypt(data, UseOaep);
            return Encoding.UTF8.GetString(decryptedData);
        }

        public string Encrypt(string toEncrypt)
        {
            var data = Encoding.UTF8.GetBytes(toEncrypt);
            var encryptedData = _sp.Encrypt(data, UseOaep);
            return encryptedData.ToBase64();
        }

        public string Encrypt(string toEncrypt, string publicKey)
        {
            return Encrypt(toEncrypt, publicKey, DefaultExponentHex);
        }

        public string Encrypt(string toEncrypt, string m, byte[] e)
        {
            var rsa = new RSACryptoServiceProvider();
            var parameters = new RSAParameters
            {
                Modulus = m.FromBase64Bytes(),
                Exponent = e
            };
            rsa.ImportParameters(parameters);
            var data = Encoding.UTF8.GetBytes(toEncrypt);
            var encryptedData = rsa.Encrypt(data, UseOaep);
            return encryptedData.ToBase64();
        }

        #endregion

        public static string EncryptDataWithPublic(string toEncrypt, string publicKey)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(new RSAParameters
            {
                Modulus = publicKey.FromBase64Bytes(),
                Exponent = DefaultExponentHex
            });
            return rsa.Encrypt(Encoding.UTF8.GetBytes(toEncrypt), UseOaep).ToBase64();
        }

        public static CryptoStream CreateEncryptionStream(Stream writeStream, string sharedSecret)
        {
            var cryptoProvider = new TripleDESCryptoServiceProvider();
            var derivedBytes = new PasswordDeriveBytes(sharedSecret, null);
            var cryptoStream = new CryptoStream(writeStream, cryptoProvider.CreateEncryptor(derivedBytes.GetBytes(16), derivedBytes.GetBytes(16)), CryptoStreamMode.Write);
            return cryptoStream;
        }

        public static CryptoStream CreateDecryptionStream(Stream readStream, string sharedSecret)
        {
            var cryptoProvider = new TripleDESCryptoServiceProvider();
            var derivedBytes = new PasswordDeriveBytes(sharedSecret, null);
            var cryptoStream = new CryptoStream(readStream, cryptoProvider.CreateDecryptor(derivedBytes.GetBytes(16), derivedBytes.GetBytes(16)), CryptoStreamMode.Read);
            return cryptoStream;
        }
    }
}