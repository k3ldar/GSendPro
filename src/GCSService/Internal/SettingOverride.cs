using AppSettings;

namespace GSendService.Internal
{
    public class SettingOverride : ISettingOverride
    {
        public bool OverrideSettingValue(in string settingName, ref object propertyValue)
        {
            if (settingName.Equals("Path"))
            {
                propertyValue = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "GSendPro", "db");
                return true;
            }

            return false;
           // throw new NotImplementedException();
        }
    }
}
