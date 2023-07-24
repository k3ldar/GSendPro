using SharedPluginFeatures;

namespace gsend.pro.Models
{
    public sealed class MCodeIndex : BaseModel
    {
        public MCodeIndex(BaseModelData modelData, string[] availableMCodes)
            : base(modelData)
        {
            AvailableMCodes = availableMCodes;
        }

        public string[] AvailableMCodes { get; }
    }
}
