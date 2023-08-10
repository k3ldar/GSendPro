using SharedPluginFeatures;
using System.Collections.Generic;
using System;

namespace GSendService.Models
{
    public sealed class ToolUsageViewModel : BaseModel
    {
        #region Constructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2208:Instantiate argument exceptions correctly", Justification = "Validating property of param so ok")]
        public ToolUsageViewModel(in BaseModelData modelData, string title, ChartModel chartModel)
            : base(modelData)
        {
            if (String.IsNullOrEmpty(title))
                throw new ArgumentNullException(nameof(title));

            Title = title;

            ChartTitle = chartModel.ChartTitle;
            DataNames = chartModel.DataNames;
            DataValues = chartModel.DataValues;
        }

        #endregion Constructors

        #region Public Properties

        public string Title { get; }

        public string ChartTitle { get; }

        public List<KeyValuePair<ChartDataType, string>> DataNames { get; }

        public Dictionary<string, List<decimal>> DataValues { get; }

        #endregion Public Properties
    }
}
