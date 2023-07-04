using System;

using GSendService.Api;

using SharedPluginFeatures;

namespace GSendService.Internal.Timings
{
    public class JobProfilesGetTimings : SystemAdminSubMenu
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

            Result += $"\rTotal Requests|{JobProfileApi._jobProfilesGet.Requests}";
            Result += $"\rFastest ms|{JobProfileApi._jobProfilesGet.Fastest}";
            Result += $"\rSlowest ms|{JobProfileApi._jobProfilesGet.Slowest}";
            Result += $"\rAverage ms|{JobProfileApi._jobProfilesGet.Average}";
            Result += $"\rTrimmed Avg ms|{JobProfileApi._jobProfilesGet.TrimmedAverage}";
            Result += $"\rTotal ms|{JobProfileApi._jobProfilesGet.Total}";

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
            return GSend.Language.Resources.TimingsJobProfileGet;
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
