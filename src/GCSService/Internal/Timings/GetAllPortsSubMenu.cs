using System;

using GSendService.Api;

using SharedPluginFeatures;

namespace GSendService.Internal.Timings
{
    public class GetAllPortsSubMenu : SystemAdminSubMenu
    {
        public override string Action()
        {
            return String.Empty;
        }

        public override string Area()
        {
            return String.Empty;
        }

        public override string Controller()
        {
            return String.Empty;
        }

        public override string Data()
        {
            string Result = "Setting|Value";

            Result += $"\rTotal Requests|{ComPortsApi._comPortTimings.Requests}";
            Result += $"\rFastest ms|{ComPortsApi._comPortTimings.Fastest}";
            Result += $"\rSlowest ms|{ComPortsApi._comPortTimings.Slowest}";
            Result += $"\rAverage ms|{ComPortsApi._comPortTimings.Average}";
            Result += $"\rTrimmed Avg ms|{ComPortsApi._comPortTimings.TrimmedAverage}";
            Result += $"\rTotal ms|{ComPortsApi._comPortTimings.Total}";

            return Result;
        }

        public override string Image()
        {
            return Constants.SystemImageStopWatch;
        }

        public override Enums.SystemAdminMenuType MenuType()
        {
            return Enums.SystemAdminMenuType.Grid;
        }

        public override string Name()
        {
            return GSend.Language.Resources.TimingsComPortsGet;
        }

        public override string ParentMenuName()
        {
            return nameof(Languages.LanguageStrings.Timings);
        }

        public override int SortOrder()
        {
            return 0;
        }
    }
}
