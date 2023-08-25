namespace GSendShared.Models
{
    public sealed class MachineServiceModel
    {
        public MachineServiceModel()
        {
            ServiceItems = new();
        }

        public MachineServiceModel(long id, long machineId, DateTime serviceDate, ServiceType serviceType, 
            long spindleHours, Dictionary<long, string> serviceItems)
        {
            Id = id;
            MachineId = machineId;
            ServiceDate = serviceDate;
            ServiceType = serviceType;
            SpindleHours = spindleHours;
            ServiceItems = serviceItems ?? throw new ArgumentNullException(nameof(serviceItems));
        }

        public long Id { get; set; }

        public long MachineId { get; set; }

        public DateTime ServiceDate { get; set; }

        public ServiceType ServiceType { get; set; }

        public long SpindleHours { get; set; }

        public Dictionary<long, string> ServiceItems { get; set; }
    }
}
