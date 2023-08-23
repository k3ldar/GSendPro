namespace GSendShared.Models
{
    public sealed class SpindleTime : ISpindleTime
    {
        public SpindleTime(long machineId, long toolProfileId, int maxRpm, DateTime startTime, DateTime finishTime)
        {
            MachineId = machineId;
            ToolProfileId = toolProfileId;
            StartTime = startTime;
            FinishTime = finishTime;
            MaxRpm = maxRpm;
        }

        public long MachineId { get; }

        public long ToolProfileId { get; }

        public int MaxRpm { get; }

        public DateTime StartTime { get; }

        public DateTime FinishTime { get; }
    }
}
