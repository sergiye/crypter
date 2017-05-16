using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Crypter
{
    internal static class Util
    {
        internal static void ShowMainInfo(Assembly asm)
        {
            Console.WriteLine();
            var ver = asm.GetName().Version;
            Console.WriteLine(asm.GetName().Name + " Version: " + ver.ToString(3) + "; Build time: " + GetBuildTime(ver).ToString("yyyy/MM/dd HH:mm:ss"));
            var title = GetAttribute<AssemblyTitleAttribute>(asm);
            if (title != null)
                Console.WriteLine(title.Title);
            Console.WriteLine();
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
