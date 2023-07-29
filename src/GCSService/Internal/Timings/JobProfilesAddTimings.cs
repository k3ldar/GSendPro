using System;

using GSendService.Api;

using SharedPluginFeatures;

namespace GSendService.Internal.Timings
{
    public class JobProfilesAddTimings : SystemAdminSubMenu
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

            Result += $"\rTotal Requests|{JobProfileApi._jobProfileAdd.Requests}";
            Result += $"\rFastest ms|{JobProfileApi._jobProfileAdd.Fastest}";
            Result += $"\rSlowest ms|{JobProfileApi._jobProfileAdd.Slowest}";
            Result += $"\rAverage ms|{JobProfileApi._jobProfileAdd.Average}";
            Result += $"\rTrimmed Avg ms|{JobProfileApi._jobProfileAdd.TrimmedAverage}";
            Result += $"\rTotal ms|{JobProfileApi._jobProfileAdd.Total}";

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
            return GSend.Language.Resources.TimingsJobProfileAdd;
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
