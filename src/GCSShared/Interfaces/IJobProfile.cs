namespace GSendShared
{
    public interface IJobProfile
    {
        long Id { get; }

        string Name { get; set; }

        string Description { get; set; }

        ulong SerialNumber { get; }
    }
}
