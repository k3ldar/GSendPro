using System;

using GSendShared;

using SharedPluginFeatures;

namespace GSendService.Models
{
    public sealed class DeleteMachineModel : BaseModel
    {
        public DeleteMachineModel()
        {
            ConfirmDelete = false;
        }

        public DeleteMachineModel(BaseModelData modelData, long id, string name, MachineType machineType,
            MachineFirmware machineFirmware, string comport, bool isConnected)
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
            IsConnected = isConnected;
            MachineFirmware = machineFirmware;
            ConfirmDelete = false;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public MachineType MachineType { get; set; }

        public MachineFirmware MachineFirmware { get; set; }

        public string ComPort { get; set; }

        public bool IsConnected { get; }

        public bool ConfirmDelete { get; set; }
    }
}
