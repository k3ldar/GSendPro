using SharedPluginFeatures;

namespace GSendService.Models
{
    public class AddLicenseModel : BaseModel
    {
        public AddLicenseModel()
        {

        }

        public AddLicenseModel(BaseModelData modelData)
            : base(modelData)
        {

        }

        public string License {  get; set; }
    }
}
