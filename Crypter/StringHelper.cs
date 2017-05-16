using System;
using System.Text;

namespace Crypter
{
    public static class StringHelper
    {
        public static string ToHexStr(this byte[] data)
        {
            var sb = new StringBuilder(data.Length * 2);
            foreach (var b in data)
                sb.Append(b.ToString("X2")); // can be "x2" if you want lowercase
            return sb.ToString();
        }

        public static byte[] HexToBytes(this string hex)
        {
            if (hex.Length == 0)
                return new byte[] {0};
            if (hex.Length%2 == 1)
                hex = "0" + hex;
            var result = new byte[hex.Length / 2];
            for (var i = 0; i < hex.Length/2; i++)
                result[i] = byte.Parse(hex.Substring(2*i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            return result;
        }

        public static bool IsHex(this string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        public static string Shorten(this string str, int maxLen)
        {
            return string.IsNullOrWhiteSpace(str) ? str : str.Length <= maxLen ? str : str.Substring(0, maxLen - 3) + "...";
        }

        public static string CapitalizeFirstLetter(this string input)
        {
            return string.IsNullOrWhiteSpace(input) ? input : input.Substring(0, 1).ToUpper() + input.Substring(1);
        }

        public static string Reverse(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            var charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string StrValue(this DateTime value)
        {
            return value == DateTime.MinValue ? "" : value.ToString("g");
        }

        public static string ToBase64(this byte[] data)
        {
            if (data == null || data.Length == 0) return null;
            return Convert.ToBase64String(data);
        }

        public static string ToBase64(this string str)
        {
            return Encoding.UTF8.GetBytes(str).ToBase64();
        }

        public static byte[] FromBase64Bytes(this string str)
        {
            return string.IsNullOrEmpty(str) ? null : Convert.FromBase64String(str);
        }

        public static string FromBase64(this string str)
        {
            return string.IsNullOrEmpty(str) ? null : Encoding.UTF8.GetString(str.FromBase64Bytes());
        }
    }
}