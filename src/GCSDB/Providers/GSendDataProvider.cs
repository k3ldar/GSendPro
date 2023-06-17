﻿using GSendDB.Tables;

using GSendShared;
using GSendShared.Models;

using Shared.Classes;

using SimpleDB;

namespace GSendDB.Providers
{
    internal class GSendDataProvider : IGSendDataProvider
    {
        private readonly ISimpleDBOperations<MachineDataRow> _machineDataRow;
        private readonly ISimpleDBOperations<MachineSpindleTimeDataRow> _spindleTimeTable;
        private readonly ISimpleDBOperations<JobProfileDataRow> _jobProfileTable;
        private readonly ISimpleDBOperations<ToolDatabaseDataRow> _toolDatabaseTable;

        public GSendDataProvider(ISimpleDBOperations<MachineDataRow> machineDataRow,
            ISimpleDBOperations<MachineSpindleTimeDataRow> spindleTimeTable,
            ISimpleDBOperations<JobProfileDataRow> jobProfileTable,
            ISimpleDBOperations<ToolDatabaseDataRow> toolDatabaseTable)
        {
            _machineDataRow = machineDataRow ?? throw new ArgumentNullException(nameof(machineDataRow));
            _spindleTimeTable = spindleTimeTable ?? throw new ArgumentNullException(nameof(spindleTimeTable));
            _jobProfileTable = jobProfileTable ?? throw new ArgumentNullException(nameof(jobProfileTable));
            _toolDatabaseTable = toolDatabaseTable ?? throw new ArgumentNullException(nameof(toolDatabaseTable));
        }

        #region Machines

        public bool MachineAdd(IMachine machine)
        {
            MachineDataRow machineDataRow = new()
            {
                ComPort = machine.ComPort,
                Name = machine.Name,
                MachineType = machine.MachineType,
            };

            _machineDataRow.Insert(machineDataRow);

            return true;
        }

        public void MachineRemove(long machineId)
        {
            MachineDataRow rowToDelete = _machineDataRow.Select(machineId);

            if (rowToDelete != null)
            {
                _machineDataRow.Delete(rowToDelete);
            }
        }

        public void MachineUpdate(IMachine machine)
        {
            if (machine == null)
                return;

            MachineDataRow rowToUpdate = ConvertFromIMachineToMachineDataRow(machine);

            if (rowToUpdate == null)
                return;

            _machineDataRow.InsertOrUpdate(rowToUpdate);
        }

        public IReadOnlyList<IMachine> MachinesGet()
        {
            return InternalGetMachines();
        }

        public IMachine MachineGet(long machineId)
        {
            return ConvertFromMachineDataRow(_machineDataRow.Select(machineId));
        }

        #endregion Machines

        #region Spindle Time

        public long SpindleTimeCreate(long machineId, int maxSpindleSpeed, long toolProfileId)
        {
            
            MachineSpindleTimeDataRow Result = new()
            {
                StartTime = DateTime.UtcNow,
                FinishTime = DateTime.MinValue,
                MachineId = machineId,
                MaxRpm = maxSpindleSpeed,
                ToolProfileId = toolProfileId,
            };

            _spindleTimeTable.Insert(Result);
            
            return Result.Id;
        }

        public void SpindleTimeFinish(long spindleTimeId)
        {
            MachineSpindleTimeDataRow spindleTime = _spindleTimeTable.Select(spindleTimeId);

            if (spindleTime != null)
            {
                spindleTime.FinishTime = DateTime.UtcNow;
                _spindleTimeTable.Update(spindleTime);
            }
        }

        #endregion SpindleTime

        #region Job Profiles

        public IJobProfile JobProfileGet(long jobId)
        {
            JobProfileDataRow jobProfile = _jobProfileTable.Select(jobId);

            return CreateJobProfileModelFromJobProfileDataRow(jobProfile);
        }

        public IReadOnlyList<IJobProfile> JobProfilesGet()
        {
            List<IJobProfile> Result = new();

            foreach (JobProfileDataRow profile in _jobProfileTable.Select())
            {
                Result.Add(CreateJobProfileModelFromJobProfileDataRow(profile));
            }

            return Result;
        }

        public long JobProfileAdd(string name, string description)
        {
            JobProfileDataRow newJobProfile = new()
            {
                JobName = name,
                JobDescription = description,
            };

            _jobProfileTable.Insert(newJobProfile);

            return newJobProfile.Id;
        }

        public void JobProfileUpdate(IJobProfile jobProfile)
        {
            JobProfileDataRow jobProfileDataRow = _jobProfileTable.Select(jobProfile.Id);

            if (jobProfileDataRow != null)
            {
                jobProfileDataRow.JobName = jobProfile.Name;
                jobProfileDataRow.JobDescription = jobProfile.Description;

                _jobProfileTable.Update(jobProfileDataRow);
            }
        }

        public void JobProfileRemove(long id)
        {
            if (_jobProfileTable.RecordCount < 2)
                return;

            JobProfileDataRow rowToRemove = _jobProfileTable.Select(id);

            if (rowToRemove == null)
                return;

            _jobProfileTable.Delete(rowToRemove);
        }

        public ulong JobProfileGetNextSerialNumber(IJobProfile jobProfile)
        {
            using (TimedLock tl = TimedLock.Lock(_jobProfileTable.TableLock))
            {
                JobProfileDataRow jobProfileDataRow = _jobProfileTable.Select(jobProfile.Id);

                if (jobProfileDataRow == null)
                    throw new InvalidOperationException();

                jobProfileDataRow.SerialNumber++;
                _jobProfileTable.Update(jobProfileDataRow);
                return jobProfileDataRow.SerialNumber;
            }
        }

        #endregion Job Profiles

        #region Tool Profiles

        public IReadOnlyList<IToolProfile> ToolsGet()
        {
            List<IToolProfile> Result = new();

            foreach (ToolDatabaseDataRow toolProfile in _toolDatabaseTable.Select())
            {
                Result.Add(CreateToolDatabaseModelFromToolDatabaseDataRow(toolProfile));
            }

            return Result;
        }

        public IToolProfile ToolGet(long toolProfileId)
        {
            return CreateToolDatabaseModelFromToolDatabaseDataRow(_toolDatabaseTable.Select(toolProfileId));
        }

        #endregion Tool Profiles

        #region Private Methods

        private static IToolProfile CreateToolDatabaseModelFromToolDatabaseDataRow(ToolDatabaseDataRow toolDatabaseDataRow)
        {
            if (toolDatabaseDataRow == null)
                return null;

            return new ToolProfileModel()
            {
                Id = toolDatabaseDataRow.Id,
                Name = toolDatabaseDataRow.ToolName,
                Description = toolDatabaseDataRow.Description,
            };
        }

        private MachineDataRow ConvertFromIMachineToMachineDataRow(IMachine machine)
        {
            if (machine == null)
                return null;

            MachineDataRow machineDataRow = _machineDataRow.Select(machine.Id);

            if (machineDataRow == null)
                machineDataRow = new MachineDataRow();

            machineDataRow.Name = machine.Name;
            machineDataRow.MachineType = machine.MachineType;
            machineDataRow.ComPort = machine.ComPort;
            machineDataRow.Options = machine.Options;
            machineDataRow.AxisCount = machine.AxisCount;
            machineDataRow.Settings = machine.Settings;
            machineDataRow.DisplayUnits = machine.DisplayUnits;
            machineDataRow.FeedbackUnits = machine.FeedbackUnit;
            machineDataRow.LayerHeightWarning = machine.LayerHeightWarning;
            machineDataRow.OverrideSpeed = machine.OverrideSpeed;
            machineDataRow.OverrideSpindle = machine.OverrideSpindle;
            machineDataRow.OverrideZDown = machine.OverrideZDownSpeed;
            machineDataRow.OverrideZUp = machine.OverrideZUpSpeed;
            machineDataRow.ConfigurationLastVerified = machine.ConfigurationLastVerified;
            machineDataRow.ProbeCommand = machine.ProbeCommand;
            machineDataRow.ProbeSpeed = machine.ProbeSpeed;
            machineDataRow.ProbeThickness = machine.ProbeThickness;
            machineDataRow.JogFeedRate = machine.JogFeedrate;
            machineDataRow.JogUnits = machine.JogUnits;
            machineDataRow.SpindleType = machine.SpindleType;
            machineDataRow.SoftStartSeconds = machine.SoftStartSeconds;
            machineDataRow.ServiceWeeks = machine.ServiceWeeks;
            machineDataRow.ServiceSpindleHours = machine.ServiceSpindleHours;

            return machineDataRow;
        }

        private List<IMachine> InternalGetMachines()
        {
            List<IMachine> Result = new();

            foreach (MachineDataRow machineDataRow in _machineDataRow.Select())
            {
                Result.Add(ConvertFromMachineDataRow(machineDataRow));
            }

            return Result;
        }

        private static IMachine ConvertFromMachineDataRow(MachineDataRow machineDataRow)
        {
            if (machineDataRow == null)
                return null;

            return new MachineModel(machineDataRow.Id, machineDataRow.Name, machineDataRow.MachineType, 
                machineDataRow.ComPort, machineDataRow.Options,
                machineDataRow.AxisCount, machineDataRow.Settings,
                machineDataRow.DisplayUnits, machineDataRow.FeedbackUnits, machineDataRow.OverrideSpeed,
                machineDataRow.OverrideSpindle, machineDataRow.OverrideZDown, machineDataRow.OverrideZUp, 
                machineDataRow.ConfigurationLastVerified, machineDataRow.LayerHeightWarning, machineDataRow.ProbeCommand, 
                machineDataRow.ProbeSpeed, machineDataRow.ProbeThickness,
                machineDataRow.JogFeedRate, machineDataRow.JogUnits,
                machineDataRow.SpindleType, machineDataRow.SoftStartSeconds,
                machineDataRow.ServiceWeeks, machineDataRow.ServiceSpindleHours);
        }

        private static JobProfileModel CreateJobProfileModelFromJobProfileDataRow(JobProfileDataRow profile)
        {
            if (profile == null)
                return null;

            return new JobProfileModel(profile.Id)
            {
                Name = profile.JobName,
                Description = profile.JobDescription,
                SerialNumber = profile.SerialNumber,
            };
        }

        #endregion Private Methods
    }
}
