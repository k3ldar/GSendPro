using System;
using System.Collections.Generic;
using System.Linq;

using GSendApi;

using GSendShared;
using GSendShared.Models;

namespace GSendTests.Mocks
{
    internal sealed class MockGSendApiWrapper : IGSendApiWrapper
    {
        public Uri ServerAddress => throw new NotImplementedException();

        public List<SpindleHoursModel> GetSpindleTime(long machineId, DateTime fromDate)
        {
            throw new NotImplementedException();
        }

        public bool IsLicenseValid()
        {
            throw new NotImplementedException();
        }

        public List<IJobProfile> JobProfilesGet()
        {
            throw new NotImplementedException();
        }

        public void MachineAdd(IMachine machine)
        {
            throw new NotImplementedException();
        }

        public bool MachineNameExists(string name)
        {
            throw new NotImplementedException();
        }

        public void MachineServiceAdd(long machineId, DateTime serviceDate, ServiceType serviceType, long spindleHours)
        {
            throw new NotImplementedException();
        }

        public List<MachineServiceModel> MachineServices(long machineId)
        {
            throw new NotImplementedException();
        }

        public List<IMachine> MachinesGet()
        {
            throw new NotImplementedException();
        }

        public void MachineUpdate(IMachine machine)
        {
            throw new NotImplementedException();
        }

        public bool SubprogramDelete(string name)
        {
            ISubprogram subProgram = Subprograms.Find(sp => sp.Name.Equals(name));

            if (subProgram == null)
                return false;

            Subprograms.Remove(subProgram);
            return true;
        }

        public bool SubprogramExists(string name)
        {
            return Subprograms.Exists(sp => sp.Name.Equals(name));
        }

        public ISubprogram SubprogramGet(string name)
        {
            return Subprograms.Find(sp => sp.Name.Equals(name));
        }

        public List<ISubprogram> SubprogramGet()
        {
            return Subprograms;
        }

        public bool SubprogramUpdate(ISubprogram subProgram)
        {
            ISubprogram existing = Subprograms.Find(sp => sp.Name.Equals(subProgram.Name));

            if (existing == null)
                return false;

            existing.Name = subProgram.Name;
            existing.Description = subProgram.Description;
            existing.Contents = subProgram.Contents;
            return true;
        }

        public List<ISubprogram> Subprograms { get; set; } = new();
        Uri IGSendApiWrapper.ServerAddress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<IToolProfile> ToolProfilesGet()
        {
            throw new NotImplementedException();
        }

        public IJobExecution JobExecutionCreate(long machineId, long toolId, long jobProfileId)
        {
            throw new NotImplementedException();
        }

        public TimeSpan JobExecutionByTool(IToolProfile toolProfile)
        {
            throw new NotImplementedException();
        }
    }
}
