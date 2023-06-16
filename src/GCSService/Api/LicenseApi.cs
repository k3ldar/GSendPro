using GSendShared.Abstractions;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace GSendService.Api
{
    public class LicenseApi : BaseController
    {
        private readonly ILicenseFactory _licenseFactory;

        public LicenseApi(ILicenseFactory licenseFactory)
        {
            _licenseFactory = licenseFactory ?? throw new ArgumentNullException(nameof(licenseFactory));
        }

        public IActionResult IsLicensed()
        {
            return GenerateJsonSuccessResponse(IsLicenseValid());
        }

        private bool IsLicenseValid()
        {
            ILicense license = _licenseFactory.GetActiveLicense();

            return license != null && license.Expires < DateTime.UtcNow;
        }
    }
}
