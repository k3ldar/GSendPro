using System.Text;

using GSendShared.Providers.Internal.Enc;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class Adafaesoijdsffadfcasdfa
    {
        private static readonly byte[] key = new byte[] { 239, 191, 189, 86, 239, 191, 107, 33, 239, 191, 189, 239, 189, 92, 8, 35, 93, 107, 50, 239, 19, 239, 189, 239, 191, 189, 239, 189, 239, 34, 239, 189 };

        public static IServiceCollection UseService(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            GenerateUniqueSerialNumber();

            if (DateTime.UtcNow > new DateTime(2027, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                throw new InvalidOperationException("End of life!");

            GenerateUniqueSerialNumber();

            Environment.SetEnvironmentVariable("gsp", Convert.ToBase64String(key));

            return services;
        }


        private static void GenerateUniqueSerialNumber()
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
