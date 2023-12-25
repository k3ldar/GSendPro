using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

using GSendShared.Models;

using SharedPluginFeatures;

using SystemAdmin.Plugin.Models;

namespace GSendService.Models
{
    public sealed class ToolModel : BaseModel
    {
        public ToolModel()
        {
            History = [];
        }

        public ToolModel(TimeSpan totalTime, List<ToolUsageHistoryModel> history)
        {
            TotalTimeUsed = totalTime;
            History = history;
        }

        public ToolModel(BaseModelData modelData, TimeSpan totalTime, List<ToolUsageHistoryModel> history)
            : base(modelData)
        {
            TotalTimeUsed = totalTime;
            History = history;
        }

        public long Id { get; set; }

        [Display(Name = nameof(GSend.Language.Resources.Name))]
        public string Name { get; set; }

        [Display(Name = nameof(GSend.Language.Resources.Description))]
        public string Description { get; set; }

        [Display(Name = nameof(GSend.Language.Resources.ToolLengthInMm))]
        public double LengthInMillimetres { get; set; }

        [Display(Name = nameof(GSend.Language.Resources.TotalTimeUsed))]
        public TimeSpan TotalTimeUsed { get; }

        [Display(Name = nameof(GSend.Language.Resources.ExpectedLifeTimeMinutes))]
        public double ExpectedLifeMinutes { get; set; }

        [Display(Name = nameof(GSend.Language.Resources.UsageLastReset))]
        public DateTime UsageLastReset { get; set; }

        public ChartViewModel Statistics { get; set; }

        public List<ToolUsageHistoryModel> History { get; }
    }
}
