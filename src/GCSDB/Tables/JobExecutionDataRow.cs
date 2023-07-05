using GSendShared;

using SimpleDB;

namespace GSendDB.Tables
{
    [Table("JobExecution", CompressionType.None, CachingStrategy.None)]
    internal class JobExecutionDataRow : TableRowDefinition
    {
        private long _machineId;
        private long _jobProfile;
        private long _toolId;
        private DateTime _jobStarted;
        private DateTime _jobFinished;
        private JobExecutionStatus _status;
        private bool _simulation;

        public DateTime StartDateTime
        {
            get => _jobStarted;

            set
            {
                if (value == _jobStarted)
                    return;

                _jobStarted = value;
                Update();
            }
        }

        public DateTime FinishDateTime
        {
            get => _jobFinished;

            set
            {
                if (value == _jobFinished)
                    return;

                _jobFinished = value;
                Update();
            }
        }

        public JobExecutionStatus Status
        {
            get => _status;

            set
            {
                if (value == _status)
                    return;

                _status = value;
                Update();
            }
        }

        [ForeignKey("Machines")]
        public long MachineId
        {
            get => _machineId;

            set
            {
                if (value == _machineId)
                    return;

                _machineId = value;
                Update();
            }
        }

        [ForeignKey("JobProfiles")]
        public long JobProfileId
        {
            get => _jobProfile;

            set
            {
                if (value == _jobProfile)
                    return;

                _jobProfile = value;
                Update();
            }
        }

        [ForeignKey("ToolDatabase")]
        public long ToolId
        {
            get => _toolId;

            set
            {
                if (value == _toolId)
                    return;

                _toolId = value;
                Update();
            }
        }

        public bool Simulation
        {
            get => _simulation;

            set
            {
                if (value == _simulation)
                    return;

                _simulation = value;
                Update();
            }
        }
    }
}
