using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace GSendShared.Helpers
{
    public static class EncryptionHelper
    {
        private static readonly byte[] key = new byte[] { 239, 191, 189, 86, 239, 191, 107, 33, 239, 191, 189, 239, 189, 92, 8, 35, 93, 107, 50, 239, 19, 239, 189, 239, 191, 189, 239, 189, 239, 34, 239, 189 };

        public static IServiceCollection SetupEncryption(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            Environment.SetEnvironmentVariable("gsp", Convert.ToBase64String(key));

            return services;
        }

    }
}
