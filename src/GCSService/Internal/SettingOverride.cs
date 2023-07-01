using System;
using System.IO;

using AppSettings;

using GSendShared;

namespace GSendService.Internal
{
    public class SettingOverride : ISettingOverride
    {
        public bool OverrideSettingValue(in string settingName, ref object propertyValue)
        {
            if (settingName.Equals("Path"))
            {
                propertyValue = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder, Constants.GSendProDbFolder);
                return true;
            }

            return false;
            // throw new NotImplementedException();
        }
    }
}
