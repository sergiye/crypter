using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace HGBase.Common {
       /// <summary>
       /// class can be used to encrypt decrypt 
       /// </summary>
       public static class Encryption {

              public static string DecryptRijndael( string inputString ) {
                     byte[ ] inputByte = Convert.FromBase64String( inputString );
                     byte[ ] outputByte = DecryptRijndael( inputByte, DefaultKeyRijndael( ) );
                     return Convert.ToBase64String( outputByte );
              }
              public static string DecryptRijndael( string inputString, byte[ ] key ) {
                     byte[ ] inputByte = Convert.FromBase64String( inputString );
                     byte[ ] outputByte = DecryptRijndael( inputByte, key );
                     return Convert.ToBase64String( outputByte );
              }
              public static byte[ ] DecryptRijndael( byte[ ] inputByte ) {
                     return DecryptRijndael( inputByte, DefaultKeyRijndael( ) );
              }
              public static byte[ ] DecryptRijndael( byte[ ] inputByte, byte[ ] key ) {

                     byte[ ] iv = { 0x70, 0x57, 0x64, 0x93, 0x71, 0x83, 0x54, 0x79, 0x26, 0x54, 0x92, 0x14, 0x96, 0x66, 0x19, 0x31 };
                     MemoryStream inputStream = new MemoryStream( );
                     inputStream.Write( inputByte, 0, inputByte.Length );
                     inputStream.Position = 0;

                     MemoryStream outputStream = new MemoryStream( );
                     byte[ ] buffer = new byte[ 128 ];

                     SymmetricAlgorithm algorithm = SymmetricAlgorithm.Create( "Rijndael" );
                     algorithm.IV = iv;
                     algorithm.Key = key;
                     ICryptoTransform transform = algorithm.CreateDecryptor( );
                     CryptoStream cryptStream = new CryptoStream( inputStream, transform, CryptoStreamMode.Read );

                     int remainingBufferLength = cryptStream.Read( buffer, 0, buffer.Length );
                     while ( remainingBufferLength > 0 ) {
                            outputStream.Write( buffer, 0, remainingBufferLength );
                            remainingBufferLength = cryptStream.Read( buffer, 0, buffer.Length );
                     }

                     cryptStream.Close( );
                     inputStream.Close( );

                     return outputStream.ToArray( );
              }
              public static List<string> DecryptRijndaelTextFile( string text_filename ) {
                     return DecryptRijndaelTextFile( text_filename, DefaultKeyRijndael( ) );
              }
              public static List<string> DecryptRijndaelTextFile( string text_filename, byte[ ] key ) {
                     FileInfo fi = new FileInfo( text_filename );
                     List<string> df = new List<string>( );
                     TextReader tr = fi.OpenText( );
                     string l;
                     while ( ( l = tr.ReadLine( ) ) != null ) {
                            df.Add( DecryptRijndael( l, key ) );
                     }
                     return df;
              }

              public static string EncryptRijndael( string inputString ) {
                     byte[ ] inputByte = Convert.FromBase64String( inputString );
                     byte[ ] outputByte = EncryptRijndael( inputByte, DefaultKeyRijndael( ) );
                     return Convert.ToBase64String( outputByte );
              }
              public static string EncryptRijndael( string inputString, byte[ ] key ) {
                     byte[ ] inputByte = Convert.FromBase64String( inputString );
                     byte[ ] outputByte = EncryptRijndael( inputByte, key );
                     return Convert.ToBase64String( outputByte );
              }
              public static byte[ ] EncryptRijndael( byte[ ] inputByte ) {
                     return EncryptRijndael( inputByte, DefaultKeyRijndael( ) );
              }
              public static byte[ ] EncryptRijndael( byte[ ] inputByte, byte[ ] key ) {
                     byte[ ] iv = { 0x70, 0x57, 0x64, 0x93, 0x71, 0x83, 0x54, 0x79, 0x26, 0x54, 0x92, 0x14, 0x96, 0x66, 0x19, 0x31 };

                     MemoryStream inputStream = new MemoryStream( );
                     inputStream.Write( inputByte, 0, inputByte.Length );
                     inputStream.Position = 0;

                     MemoryStream outputStream = new MemoryStream( );
                     byte[ ] buffer = new byte[ 128 ];

                     SymmetricAlgorithm algorithm = SymmetricAlgorithm.Create( "Rijndael" );
                     algorithm.IV = iv;
                     algorithm.Key = key;
                     ICryptoTransform transform = algorithm.CreateEncryptor( );
                     CryptoStream cryptStream = new CryptoStream( outputStream, transform, CryptoStreamMode.Write );

                     int remainingBufferLength = inputStream.Read( buffer, 0, buffer.Length );
                     while ( remainingBufferLength > 0 ) {
                            cryptStream.Write( buffer, 0, remainingBufferLength );
                            remainingBufferLength = inputStream.Read( buffer, 0, buffer.Length );
                     }
                     cryptStream.FlushFinalBlock( );

                     cryptStream.Close( );
                     inputStream.Close( );

                     return outputStream.ToArray( );
              }
              private static byte[ ] DefaultKeyRijndael( ) {
                     byte[ ] key = { 0x34, 0x4B, 0x38, 0x7C, 0x48, 0x40, 0xA2, 0x56, 0xD9, 0x95, 0x17, 0x5E, 0x53, 0x87, 0x65, 0x86 };
                     return key;
              }

              public static string Decrypt3DES( string inputString ) {
                     byte[ ] inputByte = Convert.FromBase64String( inputString );
                     byte[ ] outputByte = Decrypt3DES( inputByte, DefaultKey3DES( ) );
                     return Convert.ToBase64String( outputByte );
              }
              public static string Decrypt3DES( string inputString, byte[ ] key ) {
                     byte[ ] inputByte = Convert.FromBase64String( inputString );
                     byte[ ] outputByte = Decrypt3DES( inputByte, key );
                     return Convert.ToBase64String( outputByte );
              }
              public static byte[ ] Decrypt3DES( byte[ ] inputByte ) {
                     return Decrypt3DES( inputByte, DefaultKey3DES( ) );
              }
              public static byte[ ] Decrypt3DES( byte[ ] inputByte, byte[ ] key ) {
                     // Create a MemoryStream that is going to accept the decrypted bytes 
                     MemoryStream ms = new MemoryStream( );

                     // Create a symmetric algorithm "TripleDES". 
                     TripleDES alg = TripleDES.Create( );

                     // Set amode called ECB which does not need an Initial Vector.
                     alg.Mode = CipherMode.ECB;
                     alg.Padding = PaddingMode.None;
                     alg.Key = key;

                     // Create a CryptoStream through which we are going to be pumping our data. 
                     // CryptoStreamMode.Write means that we are going to be writing data to the stream 
                     // and the output will be written in the MemoryStream we have provided. 
                     CryptoStream cs = new CryptoStream( ms, alg.CreateDecryptor( ), CryptoStreamMode.Write );

                     // Write the data and make it do the decryption 
                     cs.Write( inputByte, 0, inputByte.Length );

                     // Close the crypto stream (or do FlushFinalBlock). 
                     // This will tell it that we have done our decryption and there is no more data
                     // coming in, and it is now a good time to remove the padding and finalize the
                     // decryption process. 
                     cs.Close( );

                     // Now get the decrypted data from the MemoryStream. 
                     // Some people make a mistake of using GetBuffer() here, which is not the right way. 
                     byte[ ] decryptedData = ms.ToArray( );

                     return decryptedData;

              }
              public static List<string> Decrypt3DESTextFile( string text_filename ) {
                     return Decrypt3DESTextFile( text_filename, DefaultKey3DES( ) );
              }
              public static List<string> Decrypt3DESTextFile( string text_filename, byte[ ] key ) {
                     FileInfo fi = new FileInfo( text_filename );
                     List<string> df = new List<string>( );
                     TextReader tr = fi.OpenText( );
                     string l;
                     while ( ( l = tr.ReadLine( ) ) != null ) {
                            df.Add( Decrypt3DES( l, key ) );
                     }
                     return df;
              }

              public static string Encrypt3DES( string inputString ) {
                     byte[ ] inputByte = Convert.FromBase64String( inputString );
                     byte[ ] outputByte = Encrypt3DES( inputByte, DefaultKey3DES( ) );
                     return Convert.ToBase64String( outputByte );
              }
              public static string Encrypt3DES( string inputString, byte[ ] key ) {
                     byte[ ] inputByte = Convert.FromBase64String( inputString );
                     byte[ ] outputByte = Encrypt3DES( inputByte, key );
                     return Convert.ToBase64String( outputByte );
              }
              public static byte[ ] Encrypt3DES( byte[ ] inputByte ) {
                     return Encrypt3DES( inputByte, DefaultKey3DES( ) );
              }
              public static byte[ ] Encrypt3DES( byte[ ] inputByte, byte[ ] key ) {
                     // Create a MemoryStream to accept the encrypted bytes 
                     MemoryStream ms = new MemoryStream( );

                     // Create a symmetric algorithm "TripleDES". 
                     TripleDES alg = TripleDES.Create( );

                     // Set amode called ECB which does not need an Initial Vector.
                     alg.Mode = CipherMode.ECB;
                     alg.Padding = PaddingMode.None;
                     alg.Key = key;

                     // Create a CryptoStream through which we are going to be pumping our data. 
                     // CryptoStreamMode.Write means that we are going to be writing data to the
                     // stream and the output will be written in the MemoryStream we have provided. 
                     CryptoStream cs = new CryptoStream( ms, alg.CreateEncryptor( ), CryptoStreamMode.Write );

                     // Write the data and make it do the encryption 
                     cs.Write( inputByte, 0, inputByte.Length );

                     // Close the crypto stream (or do FlushFinalBlock). 
                     // This will tell it that we have done our encryption and there is no more
                     // data coming in, and it is now a good time to apply the padding and finalize
                     // the encryption process. 
                     cs.Close( );

                     // Now get the encrypted data from the MemoryStream.
                     // Some people make a mistake of using GetBuffer() here, which is not the right way. 
                     byte[ ] encryptedData = ms.ToArray( );

                     return encryptedData;

              }
              private static byte[ ] DefaultKey3DES( ) {
                     byte[ ] key = { 0x34, 0x4B, 0x38, 0x7C, 0x48, 0x40, 0xA2, 0x56, 0xD9, 0x95, 0x17, 0x5E, 0x53, 0x87, 0x65, 0x86 };
                     return key;
              }

    #region TripleDES Crypting

    private static readonly byte[] AesKey = "63P5RtO7BZU+jhjD3tQbO8qU2ab486vb".FromBase64Bytes();
    private static readonly byte[] AesIV = "MZRNmm5AHcM=".FromBase64Bytes();

    public static byte[] EncryptTripleDES(this string plainText, byte[] key = null, byte[] iv = null)
    {
      if (string.IsNullOrWhiteSpace(plainText))
        return null;
      byte[] encrypted;
      using (var alg = new TripleDESCryptoServiceProvider())
      {
        alg.Key = key == null || key.Length <= 0 ? AesKey : key;
        alg.IV = iv == null || iv.Length <= 0 ? AesIV : iv;
        var encryptor = alg.CreateEncryptor(alg.Key, alg.IV);
        using (var msEncrypt = new MemoryStream())
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
          using (var swEncrypt = new StreamWriter(csEncrypt))
            swEncrypt.Write(plainText);
          encrypted = msEncrypt.ToArray();
        }
      }
      return encrypted;
    }

    public static string DecryptTripleDES(this byte[] cipherText, byte[] key = null, byte[] iv = null)
    {
      if (cipherText == null || cipherText.Length <= 0)
        return null;
      string plaintext;
      using (var alg = new TripleDESCryptoServiceProvider())
      {
        alg.Key = key == null || key.Length <= 0 ? AesKey : key;
        alg.IV = iv == null || iv.Length <= 0 ? AesIV : iv;
        var decryptor = alg.CreateDecryptor(alg.Key, alg.IV);
        using (var msDecrypt = new MemoryStream(cipherText))
        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        using (var srDecrypt = new StreamReader(csDecrypt))
          plaintext = srDecrypt.ReadToEnd();
      }
      return plaintext;
    }

    #endregion

       }
}

