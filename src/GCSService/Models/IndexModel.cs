using GSendShared;

using SharedPluginFeatures;

namespace GSendService.Models
{
    public class IndexModel : BaseModel
    {
        public IndexModel(BaseModelData modelData, IReadOnlyList<IMachine> machines)
            : base(modelData)
        {
            Machines = machines ?? throw new ArgumentNullException(nameof(machines));
        }

        public IReadOnlyList<IMachine> Machines { get; set; }
    }
}
