namespace GSendShared
{
    internal sealed class ServerConfigurationUpdated : IServerConfigurationUpdated
    {
        public event EventHandler OnServerConfigurationUpdated;

        void IServerConfigurationUpdated.ServerConfigurationUpdated()
        {
            OnServerConfigurationUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
