using SharedPluginFeatures;

namespace gsend.pro.Models
{
    public class MCodeModel : BaseModel
    {
        public MCodeModel(BaseModelData modelData, string mCode, string description, decimal[] seeAlso)
            : base(modelData)
        {
            MCode = mCode.ToUpper();
            Description = description;
            SeeAlso = seeAlso;
        }

        public string MCode { get; }

        public string Description { get; }

        public decimal[] SeeAlso { get; }
    }
}
