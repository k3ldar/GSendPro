namespace GSendShared.Models
{
    public sealed class MachineServiceModel
    {
        public MachineServiceModel()
        {
        }

        public MachineServiceModel(long machineId, DateTime serviceDate, ServiceType serviceType, long spindleHours)
        {
            MachineId = machineId;
            ServiceDate = serviceDate;
            ServiceType = serviceType;
            SpindleHours = spindleHours;
        }

        public long MachineId { get; set; }

        public DateTime ServiceDate { get; set; }

        public ServiceType ServiceType { get; set; }

        public long SpindleHours { get; set; }
    }
}
