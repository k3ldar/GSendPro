using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using GSendShared.Abstractions;

namespace GSendService.Internal
{
    internal class License : ILicense
    {
        #region Constants

        private const int DefaultRunningTests = 2;
        private const int DefaultMaximumConfiguration = 2;
        private const int DefaultMaximumPageScans = 10;
        private const int DefaultMaximumOpenEndpoints = 50;
        private const int DefaultMaximumTestsToRun = 2;
        private const int DefaultMaximumTestSchedules = 5;
        private const byte LicenseVersion1 = 1;

        #endregion Constants

        #region Private Members

        private readonly Dictionary<string, string> _options = new Dictionary<string, string>();

        #region Constructors

        public License()
        {
            _options = new Dictionary<string, string>();
        }

        #endregion Constructors

        private static readonly byte[] Header = new byte[9] { 83, 109, 111, 107, 101, 84, 101, 115, 116 };
        private const string key = "vTL9YkYt7jZduVWOB/JiumshaubM6YzdVjsZfmN3hT8=";

        internal static ILicense CreateLicense(in ILicenseFactory licenseFactory, in string licenseData)
        {
            if (licenseFactory == null)
                throw new ArgumentNullException(nameof(licenseFactory));

            License Result = new License();

            try
            {
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(DecryptString(licenseData, Convert.FromBase64String(key)))))
                {
                    using (BinaryReader binaryReader = new BinaryReader(ms))
                    {
                        byte[] header = binaryReader.ReadBytes(Header.Length);

                        if (CompareByteArrays(header, Header))
                        {
                            byte version = binaryReader.ReadByte();
                            int nameLength = binaryReader.ReadInt32();
                            Result.RegisteredUser = Encoding.UTF8.GetString(binaryReader.ReadBytes(nameLength));
                            Result.Expires = new DateTime(binaryReader.ReadInt64(), DateTimeKind.Utc);

                            if (version > LicenseVersion1)
                            {
                                // whats new here?
                            }
                        }
                    }
                }
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

        private static string DecryptString(string encryptedValue, byte[] key)
        {
            byte[] bytes = Convert.FromBase64String(encryptedValue);
            using Aes aes = Aes.Create();
            using (MemoryStream memStream = new MemoryStream(bytes))
            {
                byte[] iv = new byte[16];
                memStream.Read(iv, 0, 16);  // Pull the IV from the first 16 bytes of the encrypted value
                using (CryptoStream cryptStream = new CryptoStream(memStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(cryptStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }


        #endregion Private Members

        #region ILicense Properties


        public DateTime Expires { get; private set; }

        public string RegisteredUser { get; private set; }

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
    }
}
