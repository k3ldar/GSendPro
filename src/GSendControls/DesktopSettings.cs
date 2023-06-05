using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace GSendControls
{
    public static class DesktopSettings
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static void WriteValue(string Section, string Key, object Value)
        {
            WritePrivateProfileString(Section, Key, Value.ToString(), GetSettingsFile());
        }

        public static T ReadValue<T>(string Section, string Key, T defaultValue)
        {
            StringBuilder temp = new StringBuilder(255);
            _ = GetPrivateProfileString(Section, Key, "", temp, temp.Capacity, GetSettingsFile());

            if (temp.Length == 0)
                return defaultValue;

            try
            {
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(T));
                return (T)tc.ConvertFrom(temp.ToString());
            }
            catch
            {
                return defaultValue;
            }
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
