using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Providers.Internal.Enc;

namespace GSendLicenseGenerator
{
    internal class Program
    {
        private const byte LicenseVersion1 = 1;
        private const string headerValue = "GSend Pro";
        private static readonly byte[] Header = Encoding.ASCII.GetBytes(headerValue);
        private static readonly byte[] key = new byte[] { 239, 191, 189, 86, 239, 191, 107, 33, 239, 191, 189, 239, 189, 92, 8, 35, 93, 107, 50, 239, 19, 239, 189, 239, 191, 189, 239, 189, 239, 34, 239, 189 };


        static int Main(string[] args)
        {
            Environment.SetEnvironmentVariable("GSendProRootPath",
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder));
            GenerateUniqueSerialNumberInServiceForClientcomputer();

            string dec = AesImpl.Decrypt(File.ReadAllText(Path.Combine(Environment.GetEnvironmentVariable("GSendProRootPath"), "lic.dat")), key);

            CommandLineArgs cmdLineArgs = new(args);

            return ProcessArgs(cmdLineArgs);
        }

        private static int ProcessArgs(CommandLineArgs cmdLineArgs)
        {
            string userName = cmdLineArgs.Get("user", String.Empty);
            string dateTime = cmdLineArgs.Get("date", String.Empty);
            string uniqueId = cmdLineArgs.Get("id", String.Empty);

            string file = Path.Combine(Environment.GetEnvironmentVariable("GSendProRootPath"), "SerialNo.dat");

            uniqueId = AesImpl.Decrypt(File.ReadAllText(file), key);


            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(dateTime))
            {
                Console.WriteLine("Invalid args");
                return 1;
            }

            string[] idParts = uniqueId.Replace("\\n", "\n").Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (idParts.Length != 5)
                return -100;

            DateTime createdDate = new(Convert.ToInt64(idParts[1]), DateTimeKind.Utc);

            TimeSpan timeDiff = DateTime.UtcNow - createdDate;

            if (timeDiff.TotalSeconds > 5000)
                return -101;

            DateTime expires = DateTime.UtcNow.Date.AddDays(5);
            string license = CreateLicense(1, userName, expires, uniqueId);
            Console.WriteLine(license);

            File.WriteAllText(Path.Combine(Environment.GetEnvironmentVariable("GSendProRootPath"), "lic.dat"), license);

            string decrypted = AesImpl.Decrypt(File.ReadAllText(Path.Combine(Environment.GetEnvironmentVariable("GSendProRootPath"), "lic.dat")), key);

            return 0;
        }
        private static string CreateLicense(in byte version, in string name, in DateTime expires,
            in string id)
        {
            StringBuilder stringBuilder = new();

            for (int i = 0; i < Header.Length; i++)
                stringBuilder.Append((char)Header[i]);

            stringBuilder.Append('\r');
            stringBuilder.Append(version);
            stringBuilder.Append('\r');
            stringBuilder.Append(name.Length);
            stringBuilder.Append('\r');
            stringBuilder.Append(name);
            stringBuilder.Append('\r');
            stringBuilder.Append(expires.Ticks);
            stringBuilder.Append('\r');
            stringBuilder.Append(id.Length);
            stringBuilder.Append('\r');
            stringBuilder.Append(id);
            stringBuilder.Append('\r');

            return AesImpl.Encrypt(stringBuilder.ToString(), key);
        }








        private static void GenerateUniqueSerialNumberInServiceForClientcomputer()
        {
            string file = Path.Combine(Environment.GetEnvironmentVariable("GSendProRootPath"), "SerialNo.dat");

            if (File.Exists(file))
                return;

            char installDrive = Environment.GetEnvironmentVariable("GSendProRootPath")[0];
            DriveInfo drives = DriveInfo.GetDrives().Where(d => d.Name.StartsWith(installDrive)).First();

            StringBuilder stringBuilder = new();
            stringBuilder.Append(Guid.NewGuid().ToString("N"));
            stringBuilder.Append('\n');
            stringBuilder.Append(DateTime.UtcNow.Ticks);
            stringBuilder.Append('\n');
            stringBuilder.Append(drives.DriveFormat);
            stringBuilder.Append('\n');
            stringBuilder.Append(drives.TotalSize);
            stringBuilder.Append('\n');
            stringBuilder.Append(drives.DriveType);
            File.WriteAllText(file, AesImpl.Encrypt(stringBuilder.ToString(), key));
        }

    }
}