using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace GSendDesktop.Internal
{
    internal static class DesktopSettings
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static void WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, GetSettingsFile());
        }

        public static T ReadValue<T>(string Section, string Key, T defaultValue)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, temp.Capacity, GetSettingsFile());

            if (temp.Length == 0)
                return defaultValue;

            return (T)(object)temp.ToString();
        }

        private static string GetSettingsFile()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "GSendPro", "DeskTop");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Path.Combine(path, "GSendDesktop.ini");
        }
    }
}
