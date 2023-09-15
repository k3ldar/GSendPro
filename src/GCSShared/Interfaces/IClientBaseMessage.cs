namespace GSendShared
{
    public interface IClientBaseMessage
    {
        long CombinedHash { get; }

        string Identifier { get; }

        bool IsConnected { get; }

        object message { get; }

        string request { get; }

        decimal ServerCpuStatus { get; }

        bool success { get; }
    }
}