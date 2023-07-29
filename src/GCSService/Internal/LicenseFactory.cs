using System;
using System.IO;

using GSendShared;
using GSendShared.Abstractions;

using Shared.Classes;

namespace GSendService.Internal
{
    public class LicenseFactory : ILicenseFactory
    {
        #region Private Members

        private volatile static ILicense _activeLicense = null;
        private static readonly object _lockObject = new();
        private const string key = "vTL9YkYt7jZduVWOB/JiumshaubM6YzdVjsZfmN3hT8=";
#pragma warning disable IDE0052 // Remove unread private members
        private static readonly byte[] Header = new byte[9] { 83, 109, 111, 107, 101, 84, 101, 115, 116 };
#pragma warning restore IDE0052 // Remove unread private members
        private const byte LicenseVersion = 1;

        #endregion Private Members

        #region ILicenseFactory Methods

        /// <summary>
        /// Retrieves the currently active license for the application
        /// </summary>
        /// <returns><see cref="ILicense"/>ILicense instance</returns>
        public ILicense GetActiveLicense()
        {
            using (TimedLock timedLock = TimedLock.Lock(_lockObject))
            {
                if (_activeLicense == null)
                {
                    string activeLicense = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder, "lic.dat");

                    if (File.Exists(activeLicense))
                    {
                        ILicense existingLicense = LoadLicense(System.IO.File.ReadAllText(activeLicense));

                        if (LicenseIsValid(existingLicense))
                        {
                            _activeLicense = existingLicense;
                        }
                    }

                    if (_activeLicense == null)
                    {
                        _activeLicense = new License();
                    }
                }

                return _activeLicense;
            }
        }

        /// <summary>
        /// Loads license details in form of a byte array
        /// </summary>
        /// <param name="license"></param>
        /// <returns>Base64 encoded license string</returns>
        public ILicense LoadLicense(in string license)
        {
            if (String.IsNullOrEmpty(license))
                throw new ArgumentNullException(nameof(license));

            return License.CreateLicense(this, license);
        }

        /// <summary>
        /// Sets the currently active license
        /// </summary>
        /// <param name="license"></param>
        public void SetActiveLicense(in ILicense license)
        {
            if (license == null)
                throw new ArgumentNullException(nameof(license));

            if (!LicenseIsValid(license))
                throw new ArgumentException();

            using (TimedLock timedLock = TimedLock.Lock(_lockObject))
            {
                _activeLicense = license;
            }
        }

        /// <summary>
        /// Determines whether the license is valid or not
        /// </summary>
        /// <param name="license">ILicense instance</param>
        /// <returns>bool</returns>
        public bool LicenseIsValid(in ILicense license)
        {
            if (license == null)
                throw new ArgumentNullException(nameof(license));

            return license.Expires > DateTime.MinValue &&
                license.Expires >= DateTime.Now &&
                !String.IsNullOrEmpty(license.RegisteredUser) &&
                license.IsValid;
        }

        #endregion ILicenseFactory Methods
    }
}
