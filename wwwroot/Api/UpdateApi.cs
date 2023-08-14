using System;
using System.Net.Http.Headers;
using System.Runtime;

using gsend.pro.Internal;

using Microsoft.AspNetCore.Mvc;

using PluginManager.Abstractions;

using SharedPluginFeatures;

namespace gsend.pro.Api
{
    public class UpdateApi : BaseController
    {
        private readonly VersionSettings _versionSettings;

        public UpdateApi(ISettingsProvider settingsProvider)
        {
            if (settingsProvider == null)
                throw new ArgumentNullException(nameof(settingsProvider));

            _versionSettings = settingsProvider.GetSettings<VersionSettings>(nameof(VersionSettings));
        }

        [HttpGet]
        public IActionResult CurrentVersion()
        {
            return  Content(_versionSettings.CurrentVersion);
        }

        [HttpGet]
        public IActionResult CurrentVersionUri()
        {
            return Content(_versionSettings.LatestReleaseDownload);
        }

        [HttpGet]
        public IActionResult DownloadLatestVersion()
        {
            return new PhysicalFileResult(_versionSettings.FileLocation, "application/exe")
            {
                FileDownloadName = $"GSend.Pro.{_versionSettings.CurrentVersion}.install.exe"
            };
        }
    }
}
