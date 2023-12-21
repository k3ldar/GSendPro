using System;

using GSendApi;

using GSendCommon.Abstractions;

using GSendShared.Interfaces;

using Shared.Classes;

namespace GSendControls.Threads
{
    public sealed class ServerValidationThread : ThreadManager, IServerValidation
    {
        private const int ServerValidationRunIntervalMs = 200;
        private DateTime _nextValidation;

        public ServerValidationThread(IOnlineStatusUpdate onlineStatusUpdate)
            : base(onlineStatusUpdate, TimeSpan.FromMilliseconds(ServerValidationRunIntervalMs))
        {
            ValidateConnection();
        }

        public void ValidateConnection()
        {
            _nextValidation = DateTime.UtcNow.AddSeconds(-1);
        }

        protected override bool Run(object parameters)
        {
            if (DateTime.UtcNow < _nextValidation)
                return !HasCancelled();

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

                _nextValidation = DateTime.UtcNow.AddSeconds(10);
                return !HasCancelled();
            }

            throw new InvalidOperationException();
        }
    }
}
