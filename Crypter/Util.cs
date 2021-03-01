using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Crypter
{
    internal static class Util
    {
        internal static ConsoleColor ErrorColor = ConsoleColor.Red;
        internal static ConsoleColor SuccessColor = ConsoleColor.Green;

        internal static void WriteLine(string message = null, ConsoleColor color = ConsoleColor.White, ConsoleColor backColor = ConsoleColor.Black)
        {
            Write(message, color, backColor, true);
        }

        internal static void Write(string message = null, ConsoleColor color = ConsoleColor.White, ConsoleColor backColor = ConsoleColor.Black, bool newLine = false)
        {
            if (backColor != ConsoleColor.Black)
                Console.BackgroundColor = backColor;
            if (backColor != ConsoleColor.White)
                Console.ForegroundColor = color;
            if (newLine)
                Console.WriteLine(message);
            else
                Console.Write(message);
            Console.ResetColor();
        }

        internal static void ShowMainInfo(Assembly asm)
        {
            WriteLine();
            var ver = asm.GetName().Version;
            WriteLine(asm.GetName().Name + " Version: " + ver.ToString(3) + "; Build time: " + GetBuildTime(ver).ToString("yyyy/MM/dd HH:mm:ss"));
            var title = GetAttribute<AssemblyTitleAttribute>(asm);
            if (title != null)
                WriteLine(title.Title);
            WriteLine();
        }

        private static T GetAttribute<T>(ICustomAttributeProvider assembly, bool inherit = false) where T : Attribute
        {
            var attr = assembly.GetCustomAttributes(typeof(T), inherit);
            foreach (var o in attr)
                if (o is T)
                    return o as T;
            return null;
        }

        private static DateTime GetBuildTime(Version ver)
        {
            var buildTime = new DateTime(2000, 1, 1).AddDays(ver.Build).AddSeconds(ver.Revision * 2);
            if (TimeZone.IsDaylightSavingTime(DateTime.Now, TimeZone.CurrentTimeZone.GetDaylightChanges(DateTime.Now.Year)))
                buildTime = buildTime.AddHours(1);
            return buildTime;
        }

        internal static bool IsFileNameCorrect(string fileName)
        {
            return fileName.Any(f => Path.GetInvalidFileNameChars().Contains(f) || Path.GetInvalidPathChars().Contains(f));
        }

        internal static bool IsFileMaskUsed(string fileName)
        {
            return fileName.Any(f => new [] {'?', '*'}.Contains(f));
        }

        internal static string GetAppPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        internal static void ProcessFolder(string workPath, string searchMask, bool recursive, Action<string> fileAction)
        {
            var files = new List<string>();
            files.AddRange(Directory.GetFiles(workPath, searchMask, SearchOption.TopDirectoryOnly));
            if (files.Count > 0)
            {
                files.Sort();
                foreach (var file in files)
                    fileAction(file);
            }
            if (!recursive) return;
            foreach (var folder in Directory.GetDirectories(workPath))
                ProcessFolder(folder, searchMask, true, fileAction);
        }
    }
}
