﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using GSendShared;
using GSendShared.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.Mocks
{
    [ExcludeFromCodeCoverage]
    internal class MockGSendDataProvider : IGSendDataProvider
    {
        private readonly List<IMachine> _machines;

        public MockGSendDataProvider()
        {
            _machines = new List<IMachine>();
            JobExecutionUpdateCalled = false;
        }

        public MockGSendDataProvider(string[] machineNames)
            : this()
        {
            for (int i = 0; i < machineNames.Length; i++)
            {
                _machines.Add(new MachineModel(i, machineNames[i],
                    i % 2 == 0 ? MachineType.CNC : MachineType.Laser,
                    MachineFirmware.grblv1_1,
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

            _machines.Remove(_machines.Find(m => m.Id.Equals(machine.Id)));
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
            return _machines.Find(m => m.Id.Equals(machineId));
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
            return JobProfiles.Find(jp => jp.Id == jobId);
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
            IJobProfile storedProfile = JobProfiles.Find(jp => jp.Id.Equals(jobProfile.Id));

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

        public IToolProfile ToolGet(long toolProfileId)
        {
            return ToolProfiles.Find(tp => tp.Id.Equals(toolProfileId));
        }

        public IJobExecution JobExecutionCreate(long machineId, long toolId, long jobProfileId)
        {
            if (CreateFalseResponseWhenCalled)
                return null;

            return new JobExecutionModel(new ToolProfileModel(), new JobProfileModel(1))
            {
                Id = 123,
            };
        }

        public void JobExecutionUpdate(IJobExecution jobExecution)
        {
            Assert.IsNotNull(jobExecution);
            JobExecutionUpdateCalled = true;
        }

        public TimeSpan JobExecutionByTool(IToolProfile toolProfile)
        {
            throw new NotImplementedException();
        }

        public void ToolAdd(IToolProfile toolProfile)
        {
            throw new NotImplementedException();
        }

        public void ToolUpdate(IToolProfile toolProfile)
        {
            throw new NotImplementedException();
        }

        public void ToolResetUsage(IToolProfile toolProfile)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<JobExecutionStatistics> JobExecutionModelsGetByTool(IToolProfile toolProfile, bool sinceLastUsed)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<ISpindleTime> SpindleTimeGet(long machineId)
        {
            throw new NotImplementedException();
        }

        public void ServiceAdd(MachineServiceModel service)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<MachineServiceModel> ServicesGet(long machineId)
        {
            throw new NotImplementedException();
        }

        public List<ServiceItemModel> ServiceItemsGet(MachineType machineType)
        {
            throw new NotImplementedException();
        }

        public bool SpindleTimeCreateCalled { get; private set; }

        public bool SpindleTimeFinishCalled { get; private set; }

        public List<IJobProfile> JobProfiles { get; private set; } = new();

        public List<IToolProfile> ToolProfiles { get; private set; } = new();

        public bool CreateFalseResponseWhenCalled { get; set; }

        public bool JobExecutionUpdateCalled { get; set; }
    }
}
