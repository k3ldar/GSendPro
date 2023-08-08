using System;
using System.Collections.Generic;

using SharedPluginFeatures;

namespace GSendService.Models
{
    public class ToolsModel : BaseModel
    {
        public ToolsModel()
        {
        }

        public ToolsModel(BaseModelData modelData, List<ToolModel> tools)
            : base(modelData)
        {
            Tools = tools ?? throw new ArgumentNullException(nameof(tools));
        }

        public List<ToolModel> Tools { get; }
    }
}
