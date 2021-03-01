namespace Crypter
{
    public class SymmKeyInfo
    {
        public byte[] Key { get; }
        public byte[] Iv { get; }

        public SymmKeyInfo(byte[] key, byte[] iv)
        {
            Key = key;
            Iv = iv;
        }
    }
}