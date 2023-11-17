namespace GSendApi
{
    public interface IGSendApiWrapper : IGSendSubprogramApi,
        IGSendMachineApi,
        IGSendJobProfileApi,
        IGSendToolProfileApi,
        IGSendSpindleHoursApi,
        IGSendJobExecutionApi
    {
        Uri ServerAddress { get; set; }

        bool IsLicenseValid();

        /// <summary>
        /// Determines whether a connection can be made to the Uri
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        bool CanConnect(Uri uri);
    }
}