using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

using GSendShared;
using GSendShared.Abstractions;

namespace GSendLicenseGenerator
{
    internal class Program
    {
        private const byte LicenseVersion1 = 1;
        private const string headerValue = "GSend Pro";
        private static readonly byte[] Header = Encoding.ASCII.GetBytes(headerValue);
        private const string key = "vTL9YkYt7jZduVWOB/JiumshargM6YzdVjsZfmN3hT8=";

        static int Main(string[] args)
        {
            CommandLineArgs cmdLineArgs = new CommandLineArgs(args);

            string userName = cmdLineArgs.Get("user", String.Empty);
            string dateTime = cmdLineArgs.Get("date", String.Empty);


            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(dateTime))
            {
                Console.WriteLine("Invalid args");
                return 1;
            }

            DateTime expires = DateTime.UtcNow.Date.AddDays(5);
            string license = CreateLicense(1, userName, expires, 1000);
            Console.WriteLine(license);
            return 0;
        }
        private static string CreateLicense(in byte version, in string name, in DateTime expires,
            in int maximumMachineCount)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(ms))
                {
                    binaryWriter.Write(Header);
                    binaryWriter.Write(version);
                    binaryWriter.Write(name.Length);
                    binaryWriter.Write(Encoding.UTF8.GetBytes(name));
                    binaryWriter.Write(expires.Ticks);
                    binaryWriter.Write(maximumMachineCount);

                    ms.Position = 0;
                    byte[] licenseData = new byte[ms.Length];
                    int read = ms.Read(licenseData, 0, licenseData.Length);

                    return EncryptString(licenseData, Convert.FromBase64String(key));
                }
            }
        }

        private static string EncryptString(byte[] message, byte[] key)
        {
            using Aes aes = Aes.Create();
            byte[] iv = aes.IV;
            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(iv, 0, iv.Length);

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

    }
}