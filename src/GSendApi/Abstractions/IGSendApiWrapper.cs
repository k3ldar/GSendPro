namespace GSendApi
{
    public interface IGSendApiWrapper : IGSendSubprogramApi,
        IGSendMachineApi,
        IGSendJobProfileApi,
        IGSendToolProfileApi,
        IGSendSpindleHoursApi,
        IGSendJobExecutionApi
    {
        Uri ServerAddress { get; }

        bool IsLicenseValid();
    }
}