using System;

using GSendShared;

using SharedPluginFeatures;

namespace GSendService.Models
{
    public class ViewMachineModel : BaseModel
    {
        public ViewMachineModel(BaseModelData modelData, long id, string name, MachineType machineType, string comport,
            MachineServiceViewModel servicingModel)
           : base(modelData)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (String.IsNullOrEmpty(comport))
                throw new ArgumentNullException(nameof(comport));

            Id = id;
            Name = name;
            MachineType = machineType;
            ComPort = comport;

            ServicingModel = servicingModel ?? throw new ArgumentNullException(nameof(servicingModel));
        }

        public long Id { get; }

        public string Name { get; }

        public MachineType MachineType { get; }

        public string ComPort { get; }

        public MachineServiceViewModel ServicingModel { get; }
    }
}
