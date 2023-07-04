using System;
using System.Collections.Generic;

using GSendDesktop.Forms;

using GSendShared;
using GSendShared.Abstractions;

namespace GSendDesktop
{
    public sealed class GSendContext : IGSendContext
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<IMachine, FrmMachine> _machines = new();

        public GSendContext(IServiceProvider serviceProvider, IGSendSettings gsendSettings)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            Settings = gsendSettings ?? throw new ArgumentNullException(nameof(gsendSettings));
        }

        public void ShowMachine(IMachine machine)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            if (!_machines.ContainsKey(machine))
            {
                _machines.Add(machine, new FrmMachine(this, machine, _serviceProvider));
            }

            _machines[machine].Show();
            _machines[machine].BringToFront();
        }

        public void CloseContext()
        {
            IsClosing = true;

            foreach (KeyValuePair<IMachine, FrmMachine> keyValuePair in _machines)
            {
                keyValuePair.Value.Close();
            }

            _machines.Clear();
        }

        public bool IsClosing { get; private set; } = false;

        public IServiceProvider ServiceProvider => _serviceProvider;

        public IGSendSettings Settings { get; }
    }
}
