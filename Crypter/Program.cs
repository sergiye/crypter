using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Crypter
{
    static class Program
    {
        private static void ShowHelpText(Assembly asm)
        {
            Util.WriteLine();
            Util.WriteLine(asm.GetName().Name + " -t <some text> [-p 'some password or ? symbol'] [-d] [-w]");
            Util.WriteLine();
            Util.WriteLine("-d: decrypt input data (otherwise encrypt is used by default)");
            Util.WriteLine("-p <password/?>: password for crypting or '?' symbol to be asked for password later");

            Util.WriteLine("-t <some text>: text to convert");

            Util.WriteLine("-src <sourceFile>: source file or files mask (ex: *.txt) or path to *.txt files");
            Util.WriteLine("-r: process files in subfolders (work with -src key)");
            Util.WriteLine("-m: process file(s) in memory (with size limits)");

            Util.WriteLine("-w: wait for key press on finish");
            Util.WriteLine();
        }

        [STAThread]
        static void Main(string[] args)
        {
            var startedTime = DateTime.Now;

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
                                    Util.WriteLine("Empty '-t' parameter", Util.ErrorColor);
                                    return;
                                }
                                break;
                            case "-src":
                                if (args.Length > j + 1)
                                {
                                    srcFile = args[j + 1].Trim().Trim('\'');
                                    j++;
                                }
                                break;
                            case "-r":
                                recursive = true;
                                break;
                        }
                    }
                }

                if (string.IsNullOrEmpty(srcText) && string.IsNullOrEmpty(srcFile))
                {
                    Util.WriteLine("No input data passed", Util.ErrorColor);
                    return;
                }

                if (password.StartsWith("?"))
                {
                    password = string.Empty;
                    Util.Write("Enter your password: ");
                    ConsoleKeyInfo keyInfo;
                    do
                    {
                        keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                        {
                            password += keyInfo.KeyChar;
                            Util.Write("*");
                        }
                        else
                        {
                            if (keyInfo.Key != ConsoleKey.Backspace || password.Length <= 0) continue;
                            password = password.Substring(0, (password.Length - 1));
                            Util.Write("\b \b");
                        }
                    }
                    while (keyInfo.Key != ConsoleKey.Enter);
                    Util.WriteLine();
                    if (Debugger.IsAttached)
                        Util.WriteLine("The Password You entered is : " + password);
                }

                Util.WriteLine();
                var key = CryptoProvider.GetKey(password);
                if (!string.IsNullOrEmpty(srcText))
                {
                    Util.WriteLine(encrypt ? srcText.Encrypt(key) : srcText.Decrypt(key), Util.SuccessColor);
                }
                if (string.IsNullOrEmpty(srcFile)) return;
                if (!Util.IsFileNameCorrect(srcFile))
                {
                    Util.WriteLine("Invalid -src parameter passed", Util.ErrorColor);
                    return;
                }
                if (!Util.IsFileMaskUsed(srcFile))
                {
                    if ((File.GetAttributes(srcFile) & FileAttributes.Directory) == FileAttributes.Directory)
                        srcFile = srcFile + "/*.*";
                    else
                        recursive = false;
                }
                var workPath = Path.GetDirectoryName(srcFile);
                if (string.IsNullOrEmpty(workPath))
                    workPath = Util.GetAppPath();
                else
                    srcFile = Path.GetFileName(srcFile);
                Util.ProcessFolder(workPath, srcFile, recursive, filePath => ProcessFile(filePath, encrypt, key, memory));
            }
            catch (Exception ex)
            {
                Util.WriteLine(ex.Message, Util.ErrorColor);
            }
            finally
            {
                var timeWasted = DateTime.Now - startedTime;
                Util.WriteLine();
                Util.WriteLine(string.Format("Time wasted: {0:G}", timeWasted));
                if (wait || Debugger.IsAttached)
                {
                    Util.WriteLine();
                    Util.WriteLine("Press any key to continue...");
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
                    Util.WriteLine(string.Format("Encrypted - {0}", filePath), Util.SuccessColor);
                }
                else
                {
                    if (memory)
                        CryptoProvider.DecryptFileMem(key, filePath);
                    else
                        CryptoProvider.DecryptFile(key, filePath);
                    Util.WriteLine(string.Format("Decrypted - {0}", filePath), Util.SuccessColor);
                }
            }
            catch (Exception ex)
            {
                Util.WriteLine(string.Format("Error processing {0} - {1}", filePath, ex.Message), Util.ErrorColor);
            }
        }
    }
}
