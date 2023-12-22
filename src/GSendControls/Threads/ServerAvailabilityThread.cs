using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GSendApi;

using GSendCommon.Abstractions;

using GSendControls.Abstractions;

using Shared.Classes;

namespace GSendControls.Threads
{
    internal class ServerAvailabilityThread : ThreadManager
    {
        private readonly IGSendApiWrapper _apiWrapper;
        private readonly IServerAvailability _serverAvailability;
        private readonly ListViewItem _item;

        public ServerAvailabilityThread(IGSendApiWrapper apiWrapper, IServerAvailability serverAvailability, ListViewItem item)
            : base(null, TimeSpan.Zero)
        { 
            _apiWrapper = apiWrapper ?? throw new ArgumentNullException(nameof(apiWrapper));
            _serverAvailability = serverAvailability ?? throw new ArgumentNullException(nameof(serverAvailability));
            _item = item ?? throw new ArgumentNullException(nameof(item));
        }

        protected override bool Run(object parameters)
        {
            bool isConnected = false;

            if (_item.Tag is Uri uri)
            {
                try
                {
                    isConnected = _apiWrapper.CanConnect(uri);
                }
                catch
                {
                    // ignore, isConnected is set above
                }
            }

            _serverAvailability.UpdateServerAvailability(isConnected, _item);

            return false;
        }
    }
}
