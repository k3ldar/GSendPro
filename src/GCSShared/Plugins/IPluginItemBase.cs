using GSendShared.Models;

namespace GSendShared.Plugins
{
    /// <summary>
    /// Base plugin item interface for common methods/properties
    /// </summary>
    public interface IPluginItemBase
    {
        /// <summary>
        /// Menu name/text
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Preferred index of item in relation to other items in the menu
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Click event when menu clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Clicked();

        /// <summary>
        /// Determines whether the menu is enabled or not
        /// </summary>
        /// <returns></returns>
        bool IsEnabled();

        /// <summary>
        /// Indicates the plugin item wishes to receive Status change notifications
        /// </summary>
        bool ReceiveClientMessages { get; }

        /// <summary>
        /// Machine Status Change, if required, requires plugin options to be set correctly for entire plugin
        /// </summary>
        /// <param name="clientMessage"></param>
        void ClientMessageReceived(IClientBaseMessage clientMessage);

        /// <summary>
        /// Updates the 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="senderPluginHost"></param>
        void UpdateHost<T>(T senderPluginHost);
    }
}
