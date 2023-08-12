using System;

using SharedPluginFeatures;

using SystemAdmin.Plugin.Models;

namespace GSendService.Models
{
    public sealed class ToolModel : BaseModel
    {
        public ToolModel()
        {
        }

        public ToolModel(TimeSpan totalTime)
        {
            TotalTimeUsed = totalTime;
        }

        public ToolModel(BaseModelData modelData, TimeSpan totalTime)
            : base(modelData)
        {
            TotalTimeUsed = totalTime;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan TotalTimeUsed { get; }

        public decimal ExpectedLifeMinutes { get; set; }

        public DateTime UsageLastReset { get; set; }

        public ChartViewModel Statistics { get; set; }
    }
}
