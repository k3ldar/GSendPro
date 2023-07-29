using System;
using System.IO;

using AppSettings;

namespace gsend.pro.Internal
{
    public class SettingOverride : ISettingOverride
    {
        public bool OverrideSettingValue(in string settingName, ref object propertyValue)
        {
            if (settingName.Equals("Path"))
            {
                propertyValue = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "gsend.pro", "db");
                return true;
            }

            return false;
        }
    }
}
