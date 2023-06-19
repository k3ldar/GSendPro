using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using GSendShared;
using GSendShared.Models;

namespace GSendTests.Mocks
{
    [ExcludeFromCodeCoverage]
    internal class MockGSendDataProvider : IGSendDataProvider
    {
        private readonly List<IMachine> _machines;

        public MockGSendDataProvider()
        {
            _machines = new List<IMachine>();
        }

        public MockGSendDataProvider(string[] machineNames)
            : this()
        {
            for (int i = 0; i < machineNames.Length; i++)
            {
                _machines.Add(new MachineModel(i, machineNames[i],
                    i % 2 == 0 ? MachineType.CNC : MachineType.Laser,
                    $"COM{i + 2}",
                    MachineOptions.None, 3, new GrblSettings(),
                    FeedRateDisplayUnits.MmPerMinute,
                    FeedbackUnit.Mm,
                    60,
                    60,
                    10,
                    10,
                    DateTime.UtcNow,
                    1,
                    String.Empty,
                    10,
                    10,
                    10,
                    10,
                    SpindleType.Integrated, 0, 0, 0));
            }
        }

        public bool MachineAdd(IMachine machine)
        {
            _machines.Add(machine);

            return _machines.Contains(machine);
        }

        public void MachineUpdate(IMachine machine)
        {
            if (machine == null)
                return;

            _machines.Remove(_machines.FirstOrDefault(m => m.Id.Equals(machine.Id)));
            _machines.Add(machine);
        }

        public void MachineRemove(long machineId)
        {
            _machines.Remove(_machines.First(m => m.Id.Equals(machineId)));
        }

        public IReadOnlyList<IMachine> MachinesGet()
        {
            return _machines;
        }

        public IMachine MachineGet(long machineId)
        {
            return _machines.FirstOrDefault(m => m.Id.Equals(machineId));
        }

        public long SpindleTimeCreate(long machineId, int maxSpindleSpeed, long toolProfileId)
        {
            SpindleTimeCreateCalled = true;
            return 10;
        }

        public void SpindleTimeFinish(long spindleTimeId)
        {
            SpindleTimeFinishCalled = spindleTimeId == 10;
        }

        public IJobProfile JobProfileGet(long jobId)
        {
            return JobProfiles.FirstOrDefault(jp => jp.Id == jobId);
        }

        public IReadOnlyList<IJobProfile> JobProfilesGet()
        {
            return JobProfiles;
        }

        public long JobProfileAdd(string name, string description)
        {
            JobProfiles.Add(new JobProfileModel(JobProfiles.Count + 1)
            {
                Name = name,
                Description = description
            });

            return JobProfiles.Count;
        }

        public void JobProfileUpdate(IJobProfile jobProfile)
        {
            IJobProfile storedProfile = JobProfiles.FirstOrDefault(jp => jp.Id.Equals(jobProfile.Id));

            if (storedProfile == null)
                return;

            storedProfile.Name = jobProfile.Name;
            storedProfile.Description = jobProfile.Description;
        }

        public ulong JobProfileGetNextSerialNumber(IJobProfile jobProfile)
        {
            throw new NotImplementedException();
        }

        public void JobProfileRemove(long jobId)
        {
            JobProfiles.Remove(JobProfileGet(jobId));
        }

        public IReadOnlyList<IToolProfile> ToolsGet()
        {
            return ToolProfiles;
        }

        public IToolProfile ToolGet(long toolid)
        {
            return ToolProfiles.FirstOrDefault(tp => tp.Id.Equals(toolid));
        }

        public bool SpindleTimeCreateCalled { get; private set; }

        public bool SpindleTimeFinishCalled { get; private set; }

        public List<IJobProfile> JobProfiles { get; private set; } = new();

        public List<IToolProfile> ToolProfiles { get; private set; } = new();
    }
}
