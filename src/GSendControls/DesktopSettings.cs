﻿using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

using GSendShared;

namespace GSendControls
{
    public static class DesktopSettings
    {

#pragma warning disable SYSLIB1054

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

#pragma warning restore SYSLIB1054

        public static void WriteValue(string Section, string Key, object Value)
        {
            WritePrivateProfileString(Section, Key, Value.ToString(), GetSettingsFile());
        }

        public static T ReadValue<T>(string Section, string Key, T defaultValue)
        {
            StringBuilder temp = new(255);
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
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder, Constants.GSendProDesktopFolder);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Path.Combine(path, "GSendDesktop.ini");
        }
    }
}
