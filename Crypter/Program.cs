using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Crypter
{
    class Program
    {
        private static void ShowHelpText(Assembly asm)
        {
            Console.WriteLine();
            Console.WriteLine(asm.GetName().Name + " -t <some text> [-p 'some password'] [-d] [-w]");
            Console.WriteLine();
            Console.WriteLine("-t <some text>: text to convert");
            Console.WriteLine("-p <password>: password to use as key for crypting");
            Console.WriteLine("-src <sourceFile>: source file or files mask (ex: *.txt) or path to *.txt files");
            Console.WriteLine("-r: process files in subfolders (work with -a key)");
            //Console.WriteLine("-a: all files in app folder");

            Console.WriteLine("-d: decrypt input data (otherwise encrypt is used by default)");
            Console.WriteLine("-m: process file(s) in memory (with size limits)");

            Console.WriteLine("-w: wait for key press on finish");
            Console.WriteLine();
        }

        [STAThread]
        static void Main(string[] args)
        {
            var startedTime = DateTime.Now;

            //const string allFilesMask = "*.*";
            var wait = false;
            var recursive = false;
            var encrypt = true;
            try
            {
                var asm = Assembly.GetExecutingAssembly();
                Util.ShowMainInfo(asm);

                var password = string.Empty;
                var memory = false;
                var srcText = string.Empty;
                var srcFile = string.Empty;

                if (args.Length == 0)
                {
                    ShowHelpText(asm);
                    return;
                }
                else
                {
                    for (var j = 0; j < args.Length; j++)
                    {
                        switch (args[j].ToLower().Trim())
                        {
                            case "-w":
                                wait = true;
                                break;
                            case "-d":
                                encrypt = false;
                                break;
                            case "-m":
                                memory = true;
                                break;
                            case "-p":
                                if (args.Length > j + 1)
                                {
                                    password = args[j + 1].Trim().Trim('\'');
                                    j++;
                                }
                                break;
                            case "-t":
                                if (args.Length > j + 1)
                                {
                                    srcText = args[j + 1].Trim().Trim('\'');
                                    j++;
                                }
                                if (string.IsNullOrEmpty(srcText))
                                {
                                    Console.WriteLine("Empty '-t' parameter");
                                    return;
                                }
                                break;
                            case "-src":
                                if (args.Length > j + 1)
                                {
                                    srcFile = args[j + 1].Trim().Trim('\'');
                                    j++;
                                }
//                                if (string.IsNullOrEmpty(srcFile) || !File.Exists(srcFile))
//                                {
//                                    Console.WriteLine("Empty '-src' parameter");
//                                    return;
//                                }
                                break;
//                            case "-a":
//                                srcFile = allFilesMask;
//                                break;
                            case "-r":
                                recursive = true;
                                break;
                        }
                    }
                }

                if (string.IsNullOrEmpty(srcText) && string.IsNullOrEmpty(srcFile))
                {
                    Console.WriteLine("No input data passed");
                    return;
                }

                if (password.StartsWith("?"))
                {
                    password = string.Empty;
                    Console.Write("Enter your password: ");
                    ConsoleKeyInfo keyInfo;
                    do
                    {
                        keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                        {
                            password += keyInfo.KeyChar;
                            Console.Write("*");
                        }
                        else
                        {
                            if (keyInfo.Key != ConsoleKey.Backspace || password.Length <= 0) continue;
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                    }
                    while (keyInfo.Key != ConsoleKey.Enter);
                    Console.WriteLine();
                }
                if (Debugger.IsAttached)
                    Console.WriteLine("The Password You entered is : " + password);

                var key = CryptoProvider.GetKey(password);
                if (!string.IsNullOrEmpty(srcText))
                {
                    Console.WriteLine(encrypt ? srcText.Encrypt(key) : srcText.Decrypt(key));
                }
                if (!string.IsNullOrEmpty(srcFile))
                {
                    var workPath = Path.GetDirectoryName(srcFile);
                    if (string.IsNullOrEmpty(workPath))
                        workPath = Util.GetAppPath();
                    else
                        srcFile = Path.GetFileName(srcFile);
//                    if (string.IsNullOrEmpty(srcFile))
//                        srcFile = allFilesMask;
                    Util.ProcessFolder(workPath, srcFile, recursive, filePath => ProcessFile(filePath, encrypt, key, memory));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                var timeWasted = DateTime.Now - startedTime;
                Console.WriteLine();
                Console.WriteLine("Time wasted: {0:G}", timeWasted);
                if (wait || Debugger.IsAttached)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                 }
            }
        }

        private static void ProcessFile(string filePath, bool encode, SymmKeyInfo key, bool memory)
        {
            try
            {
                if (encode)
                {
                    if (memory)
                        CryptoProvider.EncryptFileMem(key, filePath);
                    else
                        CryptoProvider.EncryptFile(key, filePath);
                    Console.WriteLine("Encrypted - {0}", filePath);
                }
                else
                {
                    if (memory)
                        CryptoProvider.DecryptFileMem(key, filePath);
                    else
                        CryptoProvider.DecryptFile(key, filePath);
                    Console.WriteLine("Decrypted - {0}", filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing {0} - {1}", filePath, ex.Message);
            }
        }
    }
}
