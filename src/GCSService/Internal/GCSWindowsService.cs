using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Ports;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

using GSendShared;

using Microsoft.Extensions.Hosting;

using PluginManager.Abstractions;

using Shared.Classes;

using ILogger = PluginManager.Abstractions.ILogger;

namespace GSendService.Internal
{
    [ExcludeFromCodeCoverage]
    public class GCSWindowsService : BackgroundService, INotificationListener
    {
        #region Private Members

        private readonly ILogger _logger;
        private readonly List<IGCodeProcessor> _machines = new();
        private readonly IMachineProvider _machineProvider;
        private readonly IComPortFactory _comPortFactory;

        #endregion Private Members

        public GCSWindowsService(ILogger logger, IMachineProvider machineProvider, 
            IComPortFactory comPortFactory, INotificationService notificationService)
        {
            if (notificationService == null)
                throw new ArgumentNullException(nameof(notificationService));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _machineProvider = machineProvider ?? throw new ArgumentNullException(nameof(machineProvider));
            _comPortFactory = comPortFactory ?? throw new ArgumentNullException(nameof(comPortFactory));
            notificationService.RegisterListener(this);
        }

        #region Overridden Methods

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.AddToLog(PluginManager.LogLevel.Information, "GSend Background Service Starting");
            OpenProcessors();
            try
            {

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(250, stoppingToken);
                }

            }
            finally
            {
                CloseProcessors();
                _logger.AddToLog(PluginManager.LogLevel.Information, "GSend Background Service Stopping");
            }
        }

        internal void OpenProcessors()
        {
            IReadOnlyList<IMachine> machines = _machineProvider.MachinesGet();

            foreach (IMachine machine in machines) 
            {
                IGCodeProcessor processor = new GCodeProcessor(machine, _comPortFactory);
                _machines.Add(processor);
            }
        }

        internal void CloseProcessors()
        {
            _machines.ForEach(m => 
            { 
                m.Disconnect();
            });

            _machines.Clear();

            ThreadManager.CancelAll();
        }

        internal IReadOnlyList<IGCodeProcessor> Machines => _machines.AsReadOnly();

        #endregion Overridden Methods

        #region INotificationListener

        public bool EventRaised(in string eventId, in object param1, in object param2, ref object result)
        {
            return false;
        }

        public void EventRaised(in string eventId, in object param1, in object param2)
        {
            if (param1 == null || String.IsNullOrEmpty(eventId))
                return;

            long machineId = (long)param1;

            switch (eventId)
            {
                case Constants.NotificationMachineRemove:
                    RemoveMachine(machineId);

                    break;

                case Constants.NotificationMachineAdd:
                    AddMachine(machineId);

                    break;

                case Constants.NotificationMachineUpdated:
                    RemoveMachine(machineId);
                    AddMachine(machineId);

                    break;
            }

        }

        private void AddMachine(long id)
        {
            if (_machines.Any(m => m.Id == id))
                return;

            IMachine newMachine = _machineProvider.MachineGet(id);

            if (newMachine != null)
            {
                IGCodeProcessor processor = new GCodeProcessor(newMachine, _comPortFactory);
                _machines.Add(processor);
            }
        }

        private void RemoveMachine(long machineId)
        {
            IGCodeProcessor machineToDelete = _machines.Where(m => m.Id.Equals(machineId)).FirstOrDefault();

            if (machineToDelete != null)
            {
                if (machineToDelete.IsConnected)
                    machineToDelete.Disconnect();

                _machines.Remove(machineToDelete);
            }
        }

        public List<string> GetEvents()
        {
            return new List<string>()
            {
                Constants.NotificationMachineAdd,
                Constants.NotificationMachineRemove,
                Constants.NotificationMachineUpdated
            };
        }

        #endregion INotificationListener
    }
}
