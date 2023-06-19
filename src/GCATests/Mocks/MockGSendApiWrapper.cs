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
            ISubProgram subProgram = Subprograms.Where(sp => sp.Name.Equals(name)).FirstOrDefault();

            if (subProgram == null)
                return false;

            Subprograms.Remove(subProgram);
            return true;
        }

        public bool SubprogramExists(string name)
        {
            return Subprograms.Any(sp => sp.Name.Equals(name));
        }

        public ISubProgram SubprogramGet(string name)
        {
            return Subprograms.Where(sp => sp.Name.Equals(name)).FirstOrDefault();
        }

        public List<ISubProgram> SubprogramGet()
        {
            return Subprograms;
        }

        public bool SubprogramUpdate(ISubProgram subProgram)
        {
            ISubProgram existing = Subprograms.Where(sp => sp.Name.Equals(subProgram.Name)).FirstOrDefault();

            if (existing == null)
                return false;

            existing.Name = subProgram.Name;
            existing.Description = subProgram.Description;
            existing.Contents = subProgram.Contents;
            return true;
        }

        public List<ISubProgram> Subprograms { get; set; } = new();

        public List<IToolProfile> ToolProfilesGet()
        {
            throw new NotImplementedException();
        }
    }
}
