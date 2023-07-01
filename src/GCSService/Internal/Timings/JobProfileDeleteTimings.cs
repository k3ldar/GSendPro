using System;

using GSendService.Api;

using SharedPluginFeatures;

namespace GSendService.Internal.Timings
{
    public class JobProfileDeleteTimings : SystemAdminSubMenu
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

            Result += $"\rTotal Requests|{JobProfileApi._jobProfileDelete.Requests}";
            Result += $"\rFastest ms|{JobProfileApi._jobProfileDelete.Fastest}";
            Result += $"\rSlowest ms|{JobProfileApi._jobProfileDelete.Slowest}";
            Result += $"\rAverage ms|{JobProfileApi._jobProfileDelete.Average}";
            Result += $"\rTrimmed Avg ms|{JobProfileApi._jobProfileDelete.TrimmedAverage}";
            Result += $"\rTotal ms|{JobProfileApi._jobProfileDelete.Total}";

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
            return GSend.Language.Resources.TimingsJobProfileDelete;
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
