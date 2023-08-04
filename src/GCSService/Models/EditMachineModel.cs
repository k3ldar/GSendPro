using GSendShared;
using System;

using SharedPluginFeatures;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GSendService.Models
{
    public class EditMachineModel : BaseModel
    {
        public EditMachineModel()
        { 
        }

        public EditMachineModel(BaseModelData modelData, long id, string name, MachineType machineType,
            MachineFirmware machineFirmware, string comport, string[] comPorts, bool isConnected)
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
            ComPorts = comPorts;
            IsConnected = isConnected;
            MachineFirmware = machineFirmware;
        }

        public long Id { get; set; }

        [Required(ErrorMessage = nameof(GSend.Language.Resources.InvalidMachineNameLength))]
        [StringLength(GSendShared.Constants.MaximumMachineNameLength, 
            ErrorMessage = nameof(GSend.Language.Resources.InvalidMachineNameLength), 
            MinimumLength = GSendShared.Constants.MinimumMachineNameLength)]
        public string Name { get; set; }

        public MachineType MachineType { get; set; }

        public MachineFirmware MachineFirmware { get; set; }

        public string ComPort { get; set; }

        public string[] ComPorts { get; set; }

        public bool IsConnected { get; }

        public List<string> MachineTypes
        {
            get
            {
                List<string> result = new();
                
                foreach (object enumValue in Enum.GetValues(typeof(GSendShared.MachineType)))
                {
                    if (enumValue.ToString().Equals(MachineType.Unspecified.ToString()))
                        continue;

                    result.Add(enumValue.ToString());
                }
                
                return result;
            }
        }
    }
}
