using System.Collections.Concurrent;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Models;

using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;

using Shared.Classes;

namespace GSendCommon
{
    public sealed class GCodeOverrideContext : IGCodeOverrideContext
    {
        private readonly object _lockObj = new();
        private IGCodeLine _gCodeLine = null;
        private List<IMCodeOverride> _mCodeOverrides;
        private List<IGCodeOverride> _gCodeOverrides;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly IServiceProvider _serviceProvider;

        public GCodeOverrideContext(IServiceProvider serviceProvider, IStaticMethods staticMethods, IGCodeProcessor processor,
            IMachine machine, MachineStateModel machineStateModel, ConcurrentQueue<IGCodeLine> commandQueue)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            StaticMethods = staticMethods ?? throw new ArgumentNullException(nameof(staticMethods));
            Processor = processor ?? throw new ArgumentNullException(nameof(processor));
            Machine = machine ?? throw new ArgumentNullException(nameof(machine));
            MachineStateModel = machineStateModel ?? throw new ArgumentNullException(nameof(machineStateModel));
            CommandQueue = commandQueue ?? throw new ArgumentNullException(nameof(commandQueue));
        }

        public IStaticMethods StaticMethods { get; }

        public IGCodeLine GCode { get; internal set; }

        public ConcurrentQueue<IGCodeLine> CommandQueue { get; }


        public IGCodeLine OverriddenGCode
        {
            get => _gCodeLine;

            set
            {
                if (_gCodeLine != null)
                    throw new InvalidOperationException("Only one override allowed at a time");

                _gCodeLine = value;
            }
        }

        public bool Overridden => _gCodeLine != null;

        public IGCodeProcessor Processor { get; }

        public IMachine Machine { get; }

        public bool SendCommand { get; set; } = true;

        public MachineStateModel MachineStateModel { get; }

        public IJobExecution JobExecution { get; set; }


        public bool ProcessGCodeOverrides(IGCodeLine line)
        {
            using (TimedLock tl = TimedLock.Lock(_lockObj))
            {
                _cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = _cancellationTokenSource.Token;
                try
                {
                    SendCommand = true;

                    GCode = line ?? throw new ArgumentNullException(nameof(line));

                    foreach (IGCodeOverride overrideItem in CreateGCodeOverrides())
                    {
                        if (overrideItem.Process(this, cancellationToken))
                            return true;
                    }
                }
                finally
                {
                    _cancellationTokenSource?.Cancel();
                    _cancellationTokenSource = null;
                }
            }

            return false;
        }

        public bool ProcessMCodeOverrides(IGCodeLine line)
        {
            using (TimedLock tl = TimedLock.Lock(_lockObj))
            {
                _cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = _cancellationTokenSource.Token;
                try
                {
                    SendCommand = true;

                    GCode = line ?? throw new ArgumentNullException(nameof(line));
                    //add new overrides for M620, M621, M622 and blocking M623
                    foreach (IMCodeOverride overrideItem in GetMCodeOverrides())
                    {
                        if (overrideItem.Process(this, cancellationToken))
                            return true;
                    }
                }
                finally
                {
                    _cancellationTokenSource?.Cancel();
                    _cancellationTokenSource = null;
                }
            }

            return false;
        }

        public void ProcessAlarm(GrblAlarm alarm)
        {
            foreach (IGCodeOverride item in CreateGCodeOverrides())
            {
                item.Process(alarm);
            }
        }

        public void ProcessError(GrblError error)
        {
            foreach (IGCodeOverride item in CreateGCodeOverrides())
            {
                item.Process(error);
            }
        }

        public void ProcessError(Exception error)
        {
            foreach (IGCodeOverride item in CreateGCodeOverrides())
            {
                item.Process(error);
            }
        }

        public void Cancel()
        {
            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource.Cancel();
        }

        private List<IGCodeOverride> CreateGCodeOverrides()
        {
            if (_gCodeOverrides == null)
            {
                IPluginClassesService pluginClassesService = _serviceProvider.GetRequiredService<IPluginClassesService>();
                List<IGCodeOverride> Result = pluginClassesService.GetPluginClasses<IGCodeOverride>();

                _gCodeOverrides = Result.Where(o => o.MachineType.Equals(Machine.MachineType)).OrderBy(o => o.SortOrder).ToList();
            }

            return _gCodeOverrides;
        }

        private List<IMCodeOverride> GetMCodeOverrides()
        {
            if (_mCodeOverrides == null)
            {
                IPluginClassesService pluginClassesService = _serviceProvider.GetRequiredService<IPluginClassesService>();
                _mCodeOverrides = pluginClassesService.GetPluginClasses<IMCodeOverride>();
            }

            return _mCodeOverrides;
        }
    }
}
