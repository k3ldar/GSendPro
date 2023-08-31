namespace GSendShared.Models
{
    public class JobProfileModel : IJobProfile
    {
        public JobProfileModel(long id)
        {
            Id = id;
        }

        public long Id { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ulong SerialNumber { get; set; }

        public long ToolProfileId { get; set; }

        public void IncrementSerialNumber()
        {
            SerialNumber = SerialNumber + 1;
        }
    }
}
