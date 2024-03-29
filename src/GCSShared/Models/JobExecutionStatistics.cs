﻿namespace GSendShared.Models
{
    public sealed class JobExecutionStatistics
    {
        public DateTime Date { get; set; }

        public string MachineName { get; set; }

        public TimeSpan TotalTime { get; set; }

        public string ToolName { get; set; }
    }
}
