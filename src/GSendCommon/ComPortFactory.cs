using System.Reflection;

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
            ArgumentNullException.ThrowIfNull(settingsProvider);

            _settings = settingsProvider.GetSettings<GSendSettings>(Constants.SettingsName);

        }

        public IComPort CreateComPort(IMachine machine)
        {
            return new WindowsComPort(machine, _settings);
        }

        public IComPort CreateComPort(IComPortModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                if (_comPorts.ContainsKey(model.Name))
                    throw new InvalidOperationException(String.Format(ErrorComPortOpen, model.Name));

                WindowsComPort comPort = new(model);

                _comPorts.Add(model.Name, comPort);
                comPort.Open();

                return comPort;
            }
        }

        public void DeleteComPort(IComPort comPort)
        {
            ArgumentNullException.ThrowIfNull(comPort);

            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                if (!_comPorts.ContainsKey(comPort.Name))
                    throw new InvalidOperationException(String.Format(ErrorComPortNotOpen, comPort.Name));

                comPort.Close();
                _comPorts.Remove(comPort.Name);
            }
        }

        public IComPort GetComPort(string comPort)
        {
            if (String.IsNullOrEmpty(comPort))
                throw new ArgumentNullException(nameof(comPort));

            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                if (!_comPorts.TryGetValue(comPort, out IComPort _))
                    throw new InvalidOperationException(String.Format(ErrorComPortNotOpen, comPort));

                return _comPorts[comPort];
            }
        }
    }
}
