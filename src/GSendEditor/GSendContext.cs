using GSendCommon.Settings;

using GSendShared;
using GSendShared.Abstractions;

namespace GSendEditor
{
    public sealed class GSendContext : IGSendContext
    {
        private readonly IServiceProvider _serviceProvider;

        public GSendContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public void ShowMachine(IMachine machine)
        {
            throw new NotImplementedException();
        }

        public void CloseContext()
        {
            IsClosing = true;
        }

        public bool IsClosing { get; private set; } = false;

        public IServiceProvider ServiceProvider => _serviceProvider;

        public IGSendSettings Settings { get; } = new GSendSettings();
    }
}
