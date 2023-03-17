using System;
using System.Collections.Generic;

using GSendDesktop.Forms;

using GSendShared;

using Microsoft.Extensions.DependencyInjection;

namespace GSendDesktop.Internal
{
    public sealed class GSendContext : IGSendContext
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<IMachine, FrmMachine> _machines = new();

        public GSendContext(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IGSendContext>(this);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public void ShowMachine(IMachine machine)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            if (!_machines.ContainsKey(machine))
                _machines.Add(machine, new FrmMachine(this, machine));

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
    }
}
