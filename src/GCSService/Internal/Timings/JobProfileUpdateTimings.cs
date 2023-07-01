using System;

using GSendService.Api;
using SharedPluginFeatures;

namespace GSendService.Internal.Timings
{
    public class JobProfileUpdateTimings : SystemAdminSubMenu
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

            Result += $"\rTotal Requests|{JobProfileApi._jobProfileUpdate.Requests}";
            Result += $"\rFastest ms|{JobProfileApi._jobProfileUpdate.Fastest}";
            Result += $"\rSlowest ms|{JobProfileApi._jobProfileUpdate.Slowest}";
            Result += $"\rAverage ms|{JobProfileApi._jobProfileUpdate.Average}";
            Result += $"\rTrimmed Avg ms|{JobProfileApi._jobProfileUpdate.TrimmedAverage}";
            Result += $"\rTotal ms|{JobProfileApi._jobProfileUpdate.Total}";

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
            return GSend.Language.Resources.TimingsJobProfileUpdate;
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
