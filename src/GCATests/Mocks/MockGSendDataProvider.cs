﻿using System;
using System.Collections.Generic;
using System.Linq;

using GSendShared.Models;

using GSendShared;
using System.Diagnostics.CodeAnalysis;

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

        public MockGSendDataProvider(string[] names)
            : this()
        {
            for (int i = 0; i < names.Length; i++)
            {
                _machines.Add(new MachineModel(i, names[i],
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

        public long SpindleTimeCreate(long machineId, int maxSpindleSpeed)
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

        public bool SpindleTimeCreateCalled { get; private set; }

        public bool SpindleTimeFinishCalled { get; private set; }

        public List<IJobProfile> JobProfiles { get; private set; } = new();
    }
}