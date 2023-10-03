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
    }
}