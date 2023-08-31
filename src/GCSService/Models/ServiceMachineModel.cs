using GSendShared;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using System;

using SharedPluginFeatures;
using GSendShared.Models;
using System.Collections.Generic;

namespace GSendService.Models
{
    public sealed class ServiceMachineModel : BaseModel
    {
        public ServiceMachineModel()
        {
        }

        public ServiceMachineModel(BaseModelData modelData, IMachine machine, ServiceType serviceType,
            List<ServiceItemModel> serviceItems)
            : base(modelData)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            MachineId = machine.Id;
            Name = machine.Name;
            ServiceItems = serviceItems ?? throw new ArgumentNullException(nameof(serviceItems));
            ServiceType = serviceType;
        }

        public long MachineId { get; set; }

        public string Name { get; set; }

        public ServiceType ServiceType { get; set; }

        public List<ServiceItemModel> ServiceItems { get; set; }
    }
}
