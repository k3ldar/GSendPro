﻿using AppSettings;

namespace gsend.pro.Internal
{
    public class SettingsError : ISettingError
    {
        public void SettingError(in string propertyName, in string message)
        {
            // not used in this context
        }
    }
}
