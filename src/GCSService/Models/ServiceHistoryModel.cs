using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using GSendShared.Models;

using SharedPluginFeatures;

namespace GSendService.Models
{
    public sealed class ServiceHistoryModel : BaseModel
    {
        public ServiceHistoryModel()
        {

        }

        public ServiceHistoryModel(BaseModelData modelData, List<NameIdModel> machineNames, List<MachineServiceModel> history, long machineId)
            : base(modelData)
        {
            Machines = machineNames ?? throw new ArgumentNullException(nameof(machineNames));
            ServiceHistory = history ?? throw new ArgumentNullException(nameof(history));
            MachineId = machineId;
        }

        public List<NameIdModel> Machines { get; }

        public List<MachineServiceModel> ServiceHistory { get; }

        [Display(Name = nameof(GSend.Language.Resources.Machine))]
        public long MachineId { get; set; }
    }
}
