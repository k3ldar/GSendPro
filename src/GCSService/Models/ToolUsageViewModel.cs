using SharedPluginFeatures;
using System.Collections.Generic;
using System;
using GSendShared;
using GSendShared.Models;

namespace GSendService.Models
{
    public sealed class ToolUsageViewModel : BaseModel
    {
        #region Constructors

        public ToolUsageViewModel()
        {
        }
        
        public ToolUsageViewModel(in BaseModelData modelData, IToolProfile toolProfile, ChartModel chartModel, 
            ChartViewPeriod viewPeriod, ChartViewTimePeriod timePeriod, bool recentData,
            IEnumerable<JobExecutionStatistics> rawData)
            : base(modelData)
        {
            if (toolProfile == null)
                throw new ArgumentNullException(nameof(toolProfile));

            Title = toolProfile.Name;
            ToolId = toolProfile.Id;
            ChartTitle = chartModel.ChartTitle;
            DataNames = chartModel.DataNames;
            DataValues = chartModel.DataValues;
            ViewPeriod = viewPeriod;
            TimePeriod = timePeriod;
            RecentData = recentData;
            RawData = rawData ?? throw new ArgumentNullException(nameof(rawData));
        }

        #endregion Constructors

        #region Public Properties

        public long ToolId { get; set; }

        public string Title { get; }

        public string ChartTitle { get; }

        public List<KeyValuePair<ChartDataType, string>> DataNames { get; }

        public Dictionary<string, List<decimal>> DataValues { get; }

        public ChartViewPeriod ViewPeriod { get; set; }

        public ChartViewTimePeriod TimePeriod { get; set; }

        public bool RecentData { get; set; }

        public IEnumerable<JobExecutionStatistics> RawData { get; }

        #endregion Public Properties
    }
}
