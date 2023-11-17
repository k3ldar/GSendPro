using GSendShared;
using GSendShared.Models;

using Microsoft.AspNetCore.Hosting.Server;

namespace GSendApi
{
    public class GSendApiWrapper : BaseApiWrapper, IGSendApiWrapper
    {
        #region Constructors

        public GSendApiWrapper(ApiSettings apiSettings)
            : base(apiSettings)
        {
        }

        #endregion Constructors

        #region General Methods

        public bool CanConnect(Uri uri)
        {
            return CallGetApi<bool>(uri, "LicenseApi/IsLicensed/");
        }

        #endregion General Methods

        #region Machines

        public List<IMachine> MachinesGet()
        {
            return CallGetApi<List<IMachine>>("MachineApi/MachinesGet");
        }

        public void MachineAdd(IMachine machine)
        {
            CallPostApi("MachineApi/MachineAdd", machine);
        }

        public void MachineUpdate(IMachine machine)
        {
            CallPutApi("MachineApi/MachineUpdate", machine);
        }

        public bool MachineNameExists(string name)
        {
            return CallGetApi<bool>($"MachineApi/MachineExists/{name}");
        }

        #endregion Machines

        #region Services

        public List<MachineServiceModel> MachineServices(long machineId)
        {
            return CallGetApi<List<MachineServiceModel>>($"ServiceApi/ServicesGet/{machineId}");
        }

        public void MachineServiceAdd(long machineId, DateTime serviceDate, ServiceType serviceType, long spindleHours)
        {
            MachineServiceModel machineServiceModel = new()
            {
                MachineId = machineId,
                ServiceDate = serviceDate,
                ServiceType = serviceType,
                SpindleHours = spindleHours
            };

            CallPostApi("ServiceApi/ServiceAdd", machineServiceModel);
        }

        #endregion Services

        #region Spindle Time

        public List<SpindleHoursModel> GetSpindleTime(long machineId, DateTime fromDate)
        {
            return CallGetApi<List<SpindleHoursModel>>($"SpindleHoursApi/SpindleHoursGet/{machineId}/{fromDate.Ticks}/");
        }

        #endregion Spindle Time

        #region Job Profiles

        public List<IJobProfile> JobProfilesGet()
        {
            return CallGetApi<List<IJobProfile>>($"JobProfileApi/JobProfilesGet/");
        }

        #endregion Job Profiles

        #region Tool Profiles

        public List<IToolProfile> ToolProfilesGet()
        {
            return CallGetApi<List<IToolProfile>>($"ToolProfileApi/ToolsGet/");
        }

        #endregion Tool Profiles

        #region ILicense

        public bool IsLicenseValid()
        {
            return CallGetApi<bool>($"LicenseApi/IsLicensed/");
        }

        #endregion ILicense

        #region Subprograms

        public List<ISubprogram> SubprogramGet()
        {
            return CallGetApi<List<ISubprogram>>("SubprogramApi/GetAllSubprograms/");
        }

        public ISubprogram SubprogramGet(string name)
        {
            return CallGetApi<ISubprogram>($"SubprogramApi/SubprogramGet/{name}");
        }

        public bool SubprogramExists(string name)
        {
            return CallGetApi<bool>($"SubprogramApi/SubprogramExists/{name}");
        }

        public bool SubprogramDelete(string name)
        {
            return CallDeleteApi($"SubprogramApi/SubprogramDelete/{name}");
        }

        public bool SubprogramUpdate(ISubprogram subProgram)
        {
            CallPutApi<ISubprogram>($"SubprogramApi/SubprogramUpdate/", subProgram);
            return true;
        }

        #endregion Subprograms

        #region Job Execution

        public IJobExecution JobExecutionCreate(long machineId, long toolId, long jobProfileId)
        {
            return CallPostApi<IJobExecution>($"JobExecuteApi/Create/{machineId}/{toolId}/{jobProfileId}/");
        }

        public TimeSpan JobExecutionByTool(IToolProfile toolProfile)
        {
            return CallGetApi<TimeSpan>($"JobExecuteApi/ToolHours/{toolProfile.Id}/");
        }

        #endregion Job Execution
    }
}