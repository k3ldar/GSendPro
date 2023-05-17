using System;
using System.Collections.Generic;

using GSendApi;

using GSendCommon;

using GSendDesktop.Forms;

using GSendShared;
using GSendShared.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace GSendDesktop
{
    public sealed class GSendContext : IGSendContext
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<IMachine, FrmMachine> _machines = new();

        public GSendContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public void ShowMachine(IMachine machine)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            if (!_machines.ContainsKey(machine))
            {
                _machines.Add(machine, new FrmMachine(this, machine,
                    _serviceProvider.GetRequiredService<GSendApiWrapper>()));
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

        public IGsendSettings Settings { get; } = new GSendSettings();
    }
}
