using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

using Shared.Classes;

namespace GSendService.Internal
{
    public sealed class CheckForUpdatesThread : ThreadManager
    {
#if DEBUG
        private const string UpdateBaseUri = "http://localhost:5093/UpdateApi/";
#else
        private const string UpdateBaseUri = "https://www.gsend.pro/UpdateApi/";
#endif

        public CheckForUpdatesThread()
            : base(null, TimeSpan.FromMinutes(30), null, 30000, 200, false, true)
        {

        }

        protected override bool Run(object parameters)
        {
            using HttpClient httpClient = new();

            Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/text"));
            httpClient.DefaultRequestHeaders.UserAgent.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("GSend", currentVersion.ToString()));

            string address = $"{UpdateBaseUri}CurrentVersion";

            using HttpResponseMessage response = httpClient.GetAsync(address).Result;

            // if failed try again next time
            if (!response.IsSuccessStatusCode)
                return true;

            Version latestVersion = new(response.Content.ReadAsStringAsync().Result);

            if (latestVersion > currentVersion)
            {
                var responseStream = httpClient.GetStreamAsync($"{UpdateBaseUri}DownloadLatestVersion").Result;
                using var fileStream = new FileStream(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "gsend.pro.update.exe"), FileMode.Create);
                responseStream.CopyTo(fileStream);

                return false;
            }

            return !HasCancelled();
        }
    }
}
