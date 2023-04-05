using GSendShared;
using GSendShared.Interfaces;
using GSendShared.Overrides;

using Shared.Classes;

namespace GSendCommon
{
    public sealed class GCodeOverrideContext : IGCodeOverrideContext
    {
        private readonly object _lockObj = new();
        private IGCodeLine _gCodeLine = null;
        private readonly List<IGCodeOverride> _overrides;
        private CancellationTokenSource _cancellationTokenSource;

        public GCodeOverrideContext(IStaticMethods staticMethods, IGCodeProcessor processor,
            IMachine machine, IComPort comPort)
        {
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

        public bool HasCancelled { get; set; }

        public void ProcessGCodeLine(IGCodeLine line)
        {
            using (TimedLock tl = TimedLock.Lock(_lockObj))
            {
                _cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = _cancellationTokenSource.Token;
                HasCancelled = false;

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
                new SpindleActiveTime(),
                new SpindleSoftStop(),
            };

            return Result.OrderBy(o => o.SortOrder).ToList();
        }

    }
}
