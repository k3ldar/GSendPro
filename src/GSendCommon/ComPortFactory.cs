using GSendShared;

using PluginManager.Abstractions;

namespace GSendCommon
{
    public sealed class ComPortFactory : IComPortFactory
    {
        private readonly GSendSettings _settings;

        public ComPortFactory(ISettingsProvider settingsProvider)
        {
            if (settingsProvider == null)
                throw new ArgumentNullException(nameof(settingsProvider));

            _settings = settingsProvider.GetSettings<GSendSettings>(GSendShared.Constants.SettingsName);

        }

        public IComPort CreateComPort(IMachine machine)
        {
            return new WindowsComPort(machine, _settings);
        }
    }
}
