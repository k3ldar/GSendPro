﻿using AppSettings;

namespace GSendService.Internal
{
    public class ApplicationOverride : IApplicationOverride
    {
        public bool ExpandApplicationVariable(string variableName, ref object value)
        {
            //throw new NotImplementedException();
            return false;
        }
    }
}
