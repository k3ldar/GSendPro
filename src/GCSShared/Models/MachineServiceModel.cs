namespace GSendShared.Models
{
    public sealed class MachineServiceModel
    {
        public MachineServiceModel()
        {
        }

        public MachineServiceModel(long id, long machineId, DateTime serviceDate, ServiceType serviceType, long spindleHours)
        {
            Id = id;
            MachineId = machineId;
            ServiceDate = serviceDate;
            ServiceType = serviceType;
            SpindleHours = spindleHours;
        }

        public long Id { get; set; }

        public long MachineId { get; set; }

        public DateTime ServiceDate { get; set; }

        public ServiceType ServiceType { get; set; }

        public long SpindleHours { get; set; }
    }
}
