using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using GSendShared.Abstractions;

namespace GSendService.Internal
{
    internal class License : ILicense
    {
        #region Constants

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

        private static readonly byte[] Header = new byte[9] { 71, 83, 101, 110, 100, 32, 80, 114, 111 };
        private const string key = "vTL9YkYt7jZduVWOB/JiumshargM6YzdVjsZfmN3hT8=";

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
                            int idLength = binaryReader.ReadInt32();
                            Result.ClientId = Encoding.UTF8.GetString(binaryReader.ReadBytes(idLength));



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

        public string ClientId { get; private set; }

        private string _serialNo = null;

        public bool IsValid
        { 
            get
            {
                if (_serialNo == null)
                    _serialNo = GetClientUniqueID();

                return Expires.Date > DateTime.UtcNow && ClientId.Equals(_serialNo);
            }
        }

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

        private string GetClientUniqueID()
        {
            string file = Path.Combine(Environment.GetEnvironmentVariable("GSendProRootPath"), "SerialNo.dat");

            if (!File.Exists(file))
                return String.Empty;

            byte[] b = new byte[] { 71, 83, 101, 110, 100, 32, 80, 114, 111, 32, 83, 101, 114, 105, 97, 108, 32, 78, 111, 32, 45, 32, 100, 56, 57, 48, 51, 52, 50, 99, 32, 102, 110, 52, 51, 56, 53, 55, 102, 104, 110, 97, 101, 119, 115 };
            return Shared.Utilities.FileEncryptedRead(file, Encoding.UTF8.GetString(b));
        }
    }
}
