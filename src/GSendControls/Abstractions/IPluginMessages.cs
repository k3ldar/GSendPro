using GSendShared;

namespace GSendControls.Abstractions
{
    public interface IPluginMessages
    {
        /// <summary>
        /// Indicates the plugin item wishes to receive messages including Status change notifications
        /// </summary>
        bool ReceiveClientMessages { get; }

        /// <summary>
        /// Machine Status Change, if required, requires plugin options to be set correctly for entire plugin
        /// </summary>
        /// <param name="clientMessage"></param>
        void ClientMessageReceived(IClientBaseMessage clientMessage);
    }
}
