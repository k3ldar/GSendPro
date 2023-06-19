namespace GSendShared.Models
{
    public sealed class SpindleHoursModel
    {
        public long MachineId { get; set; }

        public int MaxRpm { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime FinishDateTime { get; set; }

        public long ToolProfile { get; set; }

        public TimeSpan TotalTime
        {
            get
            {
                if (FinishDateTime > StartDateTime)
                    return FinishDateTime - StartDateTime;

                return DateTime.UtcNow - StartDateTime;
            }
        }
    }
}
