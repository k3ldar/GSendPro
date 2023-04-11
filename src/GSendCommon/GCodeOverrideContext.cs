﻿using GSendCommon.Overrides;

using GSendDB.Tables;

using GSendShared;
using GSendShared.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using Shared.Classes;

using SimpleDB;

namespace GSendCommon
{
    public sealed class GCodeOverrideContext : IGCodeOverrideContext
    {
        private readonly object _lockObj = new();
        private IGCodeLine _gCodeLine = null;
        private readonly List<IGCodeOverride> _overrides;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly IServiceProvider _serviceProvider;

        public GCodeOverrideContext(IServiceProvider serviceProvider, IStaticMethods staticMethods, IGCodeProcessor processor,
            IMachine machine, IComPort comPort)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            StaticMethods = staticMethods ?? throw new ArgumentNullException(nameof(staticMethods));
            Processor = processor ?? throw new ArgumentNullException(nameof(processor));
            Machine = machine ?? throw new ArgumentNullException(nameof(machine));
            ComPort = comPort ?? throw new ArgumentNullException(nameof(comPort));

            _overrides = GetOverrides();
        }

        public IStaticMethods StaticMethods { get; }

        public IGCodeLine GCode { get; private set; }

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

        public IComPort ComPort { get; }

        public IMachine Machine { get; }

        public bool SendCommand { get; set; } = true;

        public void ProcessGCodeLine(IGCodeLine line)
        {
            using (TimedLock tl = TimedLock.Lock(_lockObj))
            {
                _cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = _cancellationTokenSource.Token;

                SendCommand = true;

                GCode = line ?? throw new ArgumentNullException(nameof(line));

                foreach (IGCodeOverride item in _overrides)
                {
                    item.Process(this, cancellationToken);
                }

                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource = null;
            }
        }

        public void ProcessAlarm(GrblAlarm alarm)
        {
            foreach (IGCodeOverride item in _overrides)
            {
                item.Process(alarm);
            }
        }

        public void ProcessError(GrblError error)
        {
            foreach (IGCodeOverride item in _overrides)
            {
                item.Process(error);
            }
        }


        public void Cancel()
        {
            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource.Cancel();
        }

        private List<IGCodeOverride> GetOverrides()
        {
            List<IGCodeOverride> Result = new()
            {
                new SpindleSoftStart(),
                new SpindleSoftStop(),
                new SpindleActiveTime(_serviceProvider.GetRequiredService<ISimpleDBOperations<MachineSpindleTimeDataRow>>()),
            };

            return Result.OrderBy(o => o.SortOrder).ToList();
        }

    }
}
