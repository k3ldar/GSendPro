namespace GSendShared.Models
{
    public sealed class SpindleHoursModel
    {
        public long MachineId { get; set; }

        public int MaxRpm { get; set; }

        public DateTime StartDateTime { get; set; }

        public TimeSpan TotalTime { get; set; }
    }
}
