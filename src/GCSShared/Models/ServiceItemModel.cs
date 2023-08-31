namespace GSendShared.Models
{
    public sealed class ServiceItemModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsDaily { get; set; }

        public bool IsMinor { get; set; }

        public bool IsMajor { get; set; }
    }
}
