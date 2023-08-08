using System;

using SharedPluginFeatures;

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

        public ToolModel(BaseModelData modelData)
            : base(modelData)
        {

        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan TotalTimeUsed { get; }

        public DateTime UsageLastReset { get; set; }
    }
}
