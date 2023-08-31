using System;

using GSendShared;

using SharedPluginFeatures;

namespace GSendService.Models
{
    public sealed class ServiceConfigurationMachineModel : BaseModel
    {
        public ServiceConfigurationMachineModel()
        {
        }

        public ServiceConfigurationMachineModel(BaseModelData modelData, IMachine machine)
            : base(modelData)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            MachineId = machine.Id;
            Name = machine.Name;
            ServiceEnabled = machine.Options.HasFlag(MachineOptions.ServiceSchedule);
            ServiceWeeks = machine.ServiceWeeks;
            SpindleHours = machine.ServiceSpindleHours;
        }

        public long MachineId { get; set; }

        public string Name { get; set; }

        public bool ServiceEnabled { get; set; }

        public int ServiceWeeks { get; set; }

        public int SpindleHours { get; set; }
    }
}
