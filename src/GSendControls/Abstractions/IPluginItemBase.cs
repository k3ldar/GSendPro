using GSendShared;

namespace GSendControls.Abstractions
{
    /// <summary>
    /// Base plugin item interface for common methods/properties
    /// </summary>
    public interface IPluginItemBase : IPluginMessages
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
        /// Determines whether the menu is enabled or not
        /// </summary>
        /// <returns></returns>
        bool IsEnabled();

        /// <summary>
        /// Updates the 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="senderPluginHost"></param>
        void UpdateHost<T>(T senderPluginHost);
    }
}
