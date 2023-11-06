using GSendApi;

namespace GSendCommon.Abstractions
{
    /// <summary>
    /// Online status interface for receiving notifications of updated status
    /// </summary>
    public interface IOnlineStatusUpdate
    {
        /// <summary>
        /// Method used to update the online status
        /// </summary>
        /// <param name="isOnline"></param>
        /// <param name="server"></param>
        void UpdateOnlineStatus(bool isOnline, string server);

        /// <summary>
        /// Api wrapper instance
        /// </summary>
        IGSendApiWrapper ApiWrapper { get; } 
    }
}
