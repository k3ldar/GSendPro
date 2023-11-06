using System;

using GSendApi;

using GSendCommon.Abstractions;

using Shared.Classes;

namespace GSendControls
{
    public sealed class ServerValidationThread : ThreadManager
    {
        public ServerValidationThread(IOnlineStatusUpdate onlineStatusUpdate)
            : base(onlineStatusUpdate, TimeSpan.FromSeconds(10))
        {

        }

        protected override bool Run(object parameters)
        {
            if (parameters is IOnlineStatusUpdate statusUpdate)
            {
                bool isError = false;
                bool isLicensed = false;
                try
                {
                    isLicensed = statusUpdate.ApiWrapper.IsLicenseValid();
                }
                catch (GSendApiException)
                {
                    isError = true;
                }

                bool isConnected = !(isError || !isLicensed);

                statusUpdate.UpdateOnlineStatus(isConnected,
                    isConnected ? statusUpdate.ApiWrapper.ServerAddress.ToString() : GSend.Language.Resources.ServerNoConnection);

                return !HasCancelled();
            }

            throw new InvalidOperationException();
        }
    }
}
