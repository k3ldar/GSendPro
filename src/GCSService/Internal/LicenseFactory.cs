using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using Shared.Classes;

using GSendShared.Abstractions;

namespace GSendService.Internal
{
    public class LicenseFactory : ILicenseFactory
    {
        #region Private Members

        private volatile static ILicense _activeLicense = null;
        private static readonly object _lockObject = new object();
        private const string key = "vTL9YkYt7jZduVWOB/JiumshaubM6YzdVjsZfmN3hT8=";
        private static readonly byte[] Header = new byte[9] { 83, 109, 111, 107, 101, 84, 101, 115, 116 };
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
                    string activeLicense = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SmokeTest", "Data", "ActiveLicense.lic");

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
        /// Retrieves license details in the form of a string
        /// </summary>
        /// <param name="license"></param>
        /// <returns></returns>
        public string SaveLicense(in ILicense license)
        {
            if (license == null)
                throw new ArgumentNullException(nameof(license));

            if (license as License == null)
                return String.Empty; 

            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(ms))
                {
                    binaryWriter.Write(Header);
                    binaryWriter.Write(LicenseVersion);
                    binaryWriter.Write(license.RegisteredUser.Length);
                    binaryWriter.Write(Encoding.UTF8.GetBytes(license.RegisteredUser));
                    binaryWriter.Write(license.Expires.Ticks);

                    ms.Position = 0;
                    byte[] licenseData = new byte[ms.Length];
                    int read = ms.Read(licenseData, 0, licenseData.Length);

                    return EncryptString(licenseData, Convert.FromBase64String(key));
                }
            }
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
                !String.IsNullOrEmpty(license.RegisteredUser);
        }

        #endregion ILicenseFactory Methods

        #region Private Static Methods

        private static string EncryptString(byte[] message, byte[] key)
        {
            using Aes aes = Aes.Create();
            byte[] iv = aes.IV;
            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(iv, 0, iv.Length);  // Add the IV to the first 16 bytes of the encrypted value
                using (CryptoStream cryptStream = new CryptoStream(memStream, aes.CreateEncryptor(key, aes.IV), CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(cryptStream))
                    {
                        writer.Write(Convert.ToBase64String(message));
                    }
                }

                byte[] buf = memStream.ToArray();
                return Convert.ToBase64String(buf, 0, buf.Length);
            }
        }


        #endregion Private Static Methods
    }
}
