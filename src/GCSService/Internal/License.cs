#if __LICENSED__
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using GSendShared.Abstractions;
using GSendShared.Providers.Internal.Enc;
#endif

namespace GSendService.Internal
{
#if __LICENSED__
    internal class License : ILicense
    {
        #region Constants

        private const byte LicenseVersion1 = 1;

        #endregion Constants

        #region Private Members

        private readonly Dictionary<string, string> _options = new();

        #region Constructors

        public License()
        {
            IsValid = false;
            _options = new Dictionary<string, string>();
        }

        #endregion Constructors

        private const int PartCount = 5;
        private const string headerValue = "GSend Pro";
        private static readonly byte[] Header = Encoding.ASCII.GetBytes(headerValue);

        internal static ILicense CreateLicense(in ILicenseFactory licenseFactory, in string licenseData)
        {
            if (licenseFactory == null)
                throw new ArgumentNullException(nameof(licenseFactory));

            License Result = new();

            try
            {
                string decryptedData = AesImpl.Decrypt(licenseData, Convert.FromBase64String(Environment.GetEnvironmentVariable("gsp")));

                string[] licParts = decryptedData.Split('\r', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                if (licParts.Length != 7)
                    throw new InvalidLicenseException("Invalid license");

                if (licParts[0] != "GSend Pro")
                    throw new InvalidLicenseException("Invalid license header");

                if (Int32.TryParse(licParts[2], out int regUserLen))
                {
                    Result.RegisteredUser = licParts[3];

                    if (regUserLen != Result.RegisteredUser.Length)
                        throw new InvalidLicenseException("Invalid registered user");
                }
                else
                {
                    throw new InvalidLicenseException("Invalid registered user reference");
                }

                if (Int64.TryParse(licParts[4], out long ticks))
                {
                    Result.Expires = new DateTime(ticks, DateTimeKind.Utc);
                }
                else
                {
                    throw new InvalidLicenseException("Invalid date reference");
                }


                if (Int32.TryParse(licParts[1], out int version))
                {
                    //if (version > LicenseVersion1)
                    //{
                    //    // whats new here?
                    //}
                }
                else
                {
                    throw new InvalidLicenseException("Invalid version reference");
                }


                Result.ClientId = licParts[6];

                string[] serialNoParts = GetClientUniqueID().Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                if (serialNoParts.Length != PartCount)
                    throw new InvalidLicenseException("Serial no invalid");

                // validate license details
                string[] idParts = Result.ClientId.Replace("\\n", "\n").Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                if (idParts.Length != PartCount)
                    throw new InvalidLicenseException("Invalid serial no");

                for (int i = 0; i < PartCount; i++)
                {
                    if (!serialNoParts[i].Equals(idParts[i]))
                        throw new InvalidLicenseException("Serial no does not match");
                }

                DateTime dateCreated = new(Convert.ToInt64(idParts[1]), DateTimeKind.Utc);

                TimeSpan createdSpan = DateTime.UtcNow - dateCreated;

                if (createdSpan.TotalSeconds < 0)
                    throw new InvalidLicenseException("Invalid create date");

                if (createdSpan.TotalDays > 365)
                    throw new InvalidLicenseException("Valid only for 1 year");

                char installDrive = Environment.GetEnvironmentVariable("GSendProRootPath")[0];
                DriveInfo drives = DriveInfo.GetDrives().Where(d => d.Name.StartsWith(installDrive)).First();

                if (idParts[2] != drives.DriveFormat)
                    throw new InvalidLicenseException("Setup has changed drive");

                if (idParts[3] != drives.TotalSize.ToString())
                    throw new InvalidLicenseException("Drive configuration changed");

                if (!drives.DriveType.ToString().StartsWith(idParts[4]))
                    throw new InvalidLicenseException("Drive type has changed");

                Result.IsValid = Result.Expires.Date > DateTime.UtcNow;
            }
            catch (FormatException)
            {

            }
            catch
            {

            }

            return Result;
        }

        private static bool CompareByteArrays(in byte[] a, in byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }

            return true;
        }


        #endregion Private Members

        #region ILicense Properties


        public DateTime Expires { get; private set; }

        public string RegisteredUser { get; private set; }

        public string ClientId { get; private set; }

        public bool IsValid { get; private set; }

        #endregion ILicense Properties

        #region ILicense Methods

        public bool OptionAvailable(in string licenseOption)
        {
            return _options.ContainsKey(licenseOption);
        }

        public string OptionValue(in string licenseOption)
        {
            if (_options.ContainsKey(licenseOption))
                return _options[licenseOption];

            throw new ArgumentException(nameof(licenseOption));
        }

        #endregion ILicense Methods

        private static string GetClientUniqueID()
        {
            string file = Path.Combine(Environment.GetEnvironmentVariable("GSendProRootPath"), "SerialNo.dat");

            if (!File.Exists(file))
                return String.Empty;

            return AesImpl.Decrypt(File.ReadAllText(file), Convert.FromBase64String(Environment.GetEnvironmentVariable("gsp")));
        }
    }
#endif
}
