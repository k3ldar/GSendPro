namespace GSendShared
{
    public interface IServerConfigurationUpdated
    {
        void ServerConfigurationUpdated();

        event EventHandler OnServerConfigurationUpdated;
    }
}
