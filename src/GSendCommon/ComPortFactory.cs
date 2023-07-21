using GSendCommon.Settings;

using GSendShared;
using GSendShared.Abstractions;

using PluginManager.Abstractions;

using Shared.Classes;

using static GSend.Language.Resources;

namespace GSendCommon
{
    public sealed class ComPortFactory : IComPortFactory
    {
        private readonly object _lockObject = new();
        private readonly Dictionary<string, IComPort> _comPorts = new();

        private readonly GSendSettings _settings;

        public ComPortFactory(ISettingsProvider settingsProvider)
        {
            if (settingsProvider == null)
                throw new ArgumentNullException(nameof(settingsProvider));

            _settings = settingsProvider.GetSettings<GSendSettings>(Constants.SettingsName);

        }

        public IComPort CreateComPort(IMachine machine)
        {
            return new WindowsComPort(machine, _settings);
        }

        public IComPort CreateComPort(IComPortModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {

                if (_comPorts.ContainsKey(model.Name))
                    throw new InvalidOperationException(String.Format(ErrorComPortOpen, model.Name));

                WindowsComPort comPort = new WindowsComPort(model);

                _comPorts.Add(model.Name, comPort);

                return comPort;
            }
        }

        public void DeleteComPort(IComPort comPort)
        {
            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                throw new NotImplementedException();
            }
        }

        public IComPort GetComPort(string comPort)
        {
            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                throw new NotImplementedException();
            }
        }
    }
}
