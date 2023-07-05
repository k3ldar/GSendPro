namespace GSendShared.Models
{
    public sealed class JobExecutionModel : IJobExecution
    {
        public JobExecutionModel()
        { 
        }

        public JobExecutionModel(IToolProfile toolProfileModel, IJobProfile jobProfileModel)
            : this()
        {
            ToolProfile = toolProfileModel ?? throw new ArgumentNullException(nameof(toolProfileModel));
            JobProfile = jobProfileModel ?? throw new ArgumentNullException(nameof(jobProfileModel));
        }

        public long Id { get; set; }

        public IMachine Machine { get; set; }

        public IJobProfile JobProfile { get; set; }

        public IToolProfile ToolProfile { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime FinishDateTime { get; set; }

        public JobExecutionStatus Status { get; set; }

        public bool Simulation { get; set; }

        public void Start(bool simulation)
        {
            Status = JobExecutionStatus.Started;
            StartDateTime = DateTime.UtcNow;
            Simulation = simulation;
        }

        public void Finish()
        {
            if (Status != JobExecutionStatus.Error)
                Status = JobExecutionStatus.Completed;

            FinishDateTime = DateTime.UtcNow;
        }
    }
}
