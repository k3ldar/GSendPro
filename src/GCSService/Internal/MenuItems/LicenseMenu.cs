#if __LICENSED__
using System;

using GSendService.Controllers;

using SharedPluginFeatures;
#endif

namespace GSendService.Internal.MenuItems
{
#if __LICENSED__
    public class LicenseMenu : MainMenuItem
    {
        public override string Action()
        {
            return nameof(HomeController.ViewLicense);
        }

        public override string Area()
        {
            return string.Empty;
        }

        public override string Controller()
        {
            return HomeController.Name;
        }

        public override string Name()
        {
            return GSend.Language.Resources.ViewLicense;
        }

        public override int SortOrder()
        {
            return Int32.MaxValue;
        }
    }
#endif
}
