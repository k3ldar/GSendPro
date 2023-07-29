using System;

using GSendShared.Abstractions;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace GSendService.Api
{
    public sealed class LicenseApi : BaseController
    {
        private readonly ILicenseFactory _licenseFactory;

        public LicenseApi(ILicenseFactory licenseFactory)
        {
            _licenseFactory = licenseFactory ?? throw new ArgumentNullException(nameof(licenseFactory));
        }

        [HttpGet]
        public IActionResult IsLicensed()
        {
            return GenerateJsonSuccessResponse(IsLicenseValid());
        }

        private bool IsLicenseValid()
        {
            ILicense license = _licenseFactory.GetActiveLicense();

            return license != null && license.IsValid;
        }
    }
}
