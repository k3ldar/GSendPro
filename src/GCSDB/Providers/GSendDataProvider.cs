using GSendDB.Tables;

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

        public GSendDataProvider(ISimpleDBOperations<MachineDataRow> machineDataRow,
            ISimpleDBOperations<MachineSpindleTimeDataRow> spindleTimeTable,
            ISimpleDBOperations<JobProfileDataRow> jobProfileTable)
        {
            _machineDataRow = machineDataRow ?? throw new ArgumentNullException(nameof(machineDataRow));
            _spindleTimeTable = spindleTimeTable ?? throw new ArgumentNullException(nameof(spindleTimeTable));
            _jobProfileTable = jobProfileTable ?? throw new ArgumentNullException(nameof(jobProfileTable));
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

        public long SpindleTimeCreate(long machineId, int maxSpindleSpeed)
        {
            MachineSpindleTimeDataRow Result = new MachineSpindleTimeDataRow()
            {
                StartTime = DateTime.UtcNow,
                FinishTime = DateTime.MinValue,
                MachineId = machineId,
                MaxRpm = maxSpindleSpeed,
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

        }

        public IReadOnlyList<IJobProfile> JobProfilesGet()
        {

        }

        public long JobProfileAdd(string name, string description)
        {
            JobProfileDataRow newJobProfile = new JobProfileDataRow()
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

        public ulong JobProfileGetNextSerialNumber(IJobProfile jobProfile)
        {
            using (TimedLock tl = TimedLock.Lock(_jobProfileTable))
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


        #region Private Methods

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

        private IMachine ConvertFromMachineDataRow(MachineDataRow machineDataRow)
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

        #endregion Private Methods
    }
}
