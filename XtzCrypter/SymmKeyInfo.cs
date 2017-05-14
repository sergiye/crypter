namespace XtzCrypter
{
    public class SymmKeyInfo
    {
        public string Key { get; }
        public string Iv { get; }

        public SymmKeyInfo()
        {
            
        }

        public SymmKeyInfo(string key, string iv)
        {
            Key = key;
            Iv = iv;
        }
    }
}