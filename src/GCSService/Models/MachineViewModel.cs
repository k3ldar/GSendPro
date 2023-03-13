using GSendShared;

using SharedPluginFeatures;

namespace GSendService.Models
{
    public class MachineViewModel : BaseModel
    {
        public MachineViewModel(BaseModelData modelData, string name)
           : base(modelData)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public string Name { get; }
    }
}
