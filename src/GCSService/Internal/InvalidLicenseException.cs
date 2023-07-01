using System;

namespace GSendService.Internal
{
    public class InvalidLicenseException : Exception
    {
        public InvalidLicenseException(string message)
            : base(message)
        {
        }
    }
}
