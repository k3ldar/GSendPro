using System.Collections.Concurrent;

using GSendShared;

using Shared.Classes;

namespace GSendService.Internal
{
    public class GCodeProcessor : ThreadManager, IGCodeProcessor
    {
        private const int QueueProcessMilliseconds = 20;
        private const int BufferSize = 120;

        private readonly ConcurrentQueue<IGCodeCommand> _commandsToSend = new();
        
        private readonly IMachine _machine;
        private readonly IComPort _port;

        public GCodeProcessor(IMachine machine, IComPortFactory comPortFactory)
            : base(machine, TimeSpan.FromMilliseconds(QueueProcessMilliseconds))
        {
            if (comPortFactory == null)
                throw new ArgumentNullException(nameof(comPortFactory));

            _machine = machine ?? throw new ArgumentNullException(nameof(machine));
            _port = comPortFactory.CreateComPort(_machine);
        }

        public bool Connect()
        {
            if (_port.IsOpen())
                return true;

            _port.Open();
            return _port.IsOpen();
        }

        public bool Disconnect()
        {
            if (!_port.IsOpen())
                return true;

            _port.Close();
            return !_port.IsOpen();
        }

        public bool IsConnected => _port.IsOpen();

        public long Id => _machine.Id;
    }
}
