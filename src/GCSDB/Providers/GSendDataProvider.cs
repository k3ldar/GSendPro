using GSendDB.Tables;

using GSendShared;
using GSendShared.Models;

using Microsoft.AspNetCore.Mvc;

using Shared.Classes;

using SharedPluginFeatures;

using SimpleDB;

namespace GSendDB.Providers
{
    internal class GSendDataProvider : IGSendDataProvider
    {
        private readonly ISimpleDBOperations<MachineDataRow> _machineDataRow;
        private readonly ISimpleDBOperations<MachineSpindleTimeDataRow> _spindleTimeTable;
        private readonly ISimpleDBOperations<JobProfileDataRow> _jobProfileTable;
        private readonly ISimpleDBOperations<ToolDatabaseDataRow> _toolDatabaseTable;
        private readonly ISimpleDBOperations<JobExecutionDataRow> _jobExecutionTable;
        private readonly ISimpleDBOperations<MachineServiceDataRow> _machineServiceTable;
        private readonly ISimpleDBOperations<ServiceItemsDataRow> _serviceItemsTable;
        private readonly IMemoryCache _memoryCache;

        public GSendDataProvider(ISimpleDBOperations<MachineDataRow> machineDataRow,
            ISimpleDBOperations<MachineSpindleTimeDataRow> spindleTimeTable,
            ISimpleDBOperations<JobProfileDataRow> jobProfileTable,
            ISimpleDBOperations<ToolDatabaseDataRow> toolDatabaseTable,
            ISimpleDBOperations<JobExecutionDataRow> jobExecutionTable,
            ISimpleDBOperations<MachineServiceDataRow> machineServiceTable,
            ISimpleDBOperations<ServiceItemsDataRow> serviceItemsTable,
            IMemoryCache memoryCache)
        {
            _machineDataRow = machineDataRow ?? throw new ArgumentNullException(nameof(machineDataRow));
            _spindleTimeTable = spindleTimeTable ?? throw new ArgumentNullException(nameof(spindleTimeTable));
            _jobProfileTable = jobProfileTable ?? throw new ArgumentNullException(nameof(jobProfileTable));
            _toolDatabaseTable = toolDatabaseTable ?? throw new ArgumentNullException(nameof(toolDatabaseTable));
            _jobExecutionTable = jobExecutionTable ?? throw new ArgumentNullException(nameof(jobExecutionTable));
            _machineServiceTable = machineServiceTable ?? throw new ArgumentNullException(nameof(machineServiceTable));
            _serviceItemsTable = serviceItemsTable ?? throw new ArgumentNullException(nameof(serviceItemsTable));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        #region Job Execution

        public IJobExecution JobExecutionCreate(long machineId, long toolId, long jobProfileId)
        {
            JobExecutionDataRow jobExecutionDataRow = new()
            {
                JobProfileId = jobProfileId,
                MachineId = machineId,
                ToolId = toolId,
                Status = JobExecutionStatus.None
            };

            _jobExecutionTable.Insert(jobExecutionDataRow);

            JobExecutionModel jobExecutionModel = new(
                CreateToolDatabaseModelFromToolDatabaseDataRow(_toolDatabaseTable.Select(toolId)),
                CreateJobProfileModelFromJobProfileDataRow(_jobProfileTable.Select(jobProfileId)))
            {
                Id = jobExecutionDataRow.Id,
                Machine = ConvertFromMachineDataRow(_machineDataRow.Select(machineId)),
            };
            
            _memoryCache.GetExtendingCache().Clear();
            return jobExecutionModel;
        }

        public void JobExecutionUpdate(IJobExecution jobExecution)
        {
            ArgumentNullException.ThrowIfNull(jobExecution);

            JobExecutionDataRow jobExecutionDataRow = _jobExecutionTable.Select(jobExecution.Id);

            jobExecutionDataRow.Status = jobExecution.Status;
            jobExecutionDataRow.StartDateTime = jobExecution.StartDateTime;
            jobExecutionDataRow.FinishDateTime = jobExecution.FinishDateTime;
            jobExecutionDataRow.Simulation = jobExecution.Simulation;

            _jobExecutionTable.Update(jobExecutionDataRow);
            _memoryCache.GetExtendingCache().Clear();
        }

        public TimeSpan JobExecutionByTool(IToolProfile toolProfile)
        {
            ArgumentNullException.ThrowIfNull(toolProfile);

            string cacheName = $"Tool execution: {toolProfile.Id} {toolProfile.Name}";

            CacheItem cacheItem = _memoryCache.GetExtendingCache().Get(cacheName);

            if (cacheItem == null)
            {
                var rows = _jobExecutionTable.Select(je => je.StartDateTime >= toolProfile.UsageLastReset &&
                    je.ToolId == toolProfile.Id && !je.Simulation && je.FinishDateTime > DateTime.MinValue);
                long ticks = rows.Sum(je => je.FinishDateTime.Ticks - je.StartDateTime.Ticks);

                cacheItem = new CacheItem(cacheName, ticks);
                _memoryCache.GetExtendingCache().Add(cacheName, cacheItem);
            }

            return new TimeSpan((long)cacheItem.Value);
        }

        public IEnumerable<JobExecutionStatistics> JobExecutionModelsGetByTool(IToolProfile toolProfile, bool sinceLastUsed)
        {
            ArgumentNullException.ThrowIfNull(toolProfile);

            string cacheName = $"Tool execution statistics: {toolProfile.Id} {toolProfile.Name} {sinceLastUsed}";

            CacheItem cacheItem = _memoryCache.GetExtendingCache().Get(cacheName);

            if (cacheItem == null)
            {
                if (sinceLastUsed)
                {
                    IEnumerable<JobExecutionStatistics> result = from je in _jobExecutionTable.Select()
                           where je.StartDateTime >= toolProfile.UsageLastReset && je.ToolId == toolProfile.Id &&
                              !je.Simulation && je.FinishDateTime > DateTime.MinValue
                           join m in _machineDataRow.Select() on je.MachineId equals m.Id
                           select new JobExecutionStatistics()
                           {
                               Date = je.FinishDateTime.Date,
                               MachineName = m.Name,
                               ToolName = toolProfile.Name,
                               TotalTime = je.FinishDateTime - je.StartDateTime
                           };
                    cacheItem = new CacheItem(cacheName, result);
                }
                else
                {
                    IEnumerable<JobExecutionStatistics> result = from je in _jobExecutionTable.Select()
                           where je.ToolId == toolProfile.Id && !je.Simulation && je.FinishDateTime > DateTime.MinValue
                           join m in _machineDataRow.Select() on je.MachineId equals m.Id
                           select new JobExecutionStatistics()
                           {
                               Date = je.FinishDateTime.Date,
                               MachineName = m.Name,
                               ToolName = toolProfile.Name,
                               TotalTime = je.FinishDateTime - je.StartDateTime
                           };
                    cacheItem = new CacheItem(cacheName, result);
                }

                _memoryCache.GetExtendingCache().Add(cacheName, cacheItem);
            }

            return (IEnumerable<JobExecutionStatistics>)cacheItem.Value;
        }

        #endregion Job Execution

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

            if (machine is MachineModel machineModel)
                machineModel.Id = machineDataRow.Id;

            return true;
        }

        public void MachineRemove(long machineId)
        {
            MachineDataRow rowToDelete = _machineDataRow.Select(machineId);

            if (rowToDelete != null)
            {
                rowToDelete.IsDeleted = true;
                _machineDataRow.Update(rowToDelete);
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
            IReadOnlyList<MachineDataRow> machines = _machineDataRow.Select(m => m.Id == machineId && !m.IsDeleted);

            if (machines == null || machines.Count == 0)
                return null;

            return ConvertFromMachineDataRow(machines[0]);
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

        public IReadOnlyList<ISpindleTime> SpindleTimeGet(long machineId)
        {
            List<MachineSpindleTimeDataRow> spindleTime = _spindleTimeTable.Select(m => m.MachineId == machineId).ToList();

            List<ISpindleTime> Result = [];

            foreach (MachineSpindleTimeDataRow row in spindleTime)
            {
                Result.Add(new SpindleTime(row.MachineId, row.ToolProfileId, row.MaxRpm, row.StartTime, row.FinishTime));
            }

            return Result;
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
            List<IJobProfile> Result = [];

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

        public void JobProfileRemove(long jobId)
        {
            if (_jobProfileTable.RecordCount < 2)
                return;

            JobProfileDataRow rowToRemove = _jobProfileTable.Select(jobId);

            if (rowToRemove == null)
                return;

            _jobProfileTable.Delete(rowToRemove);
        }

        public ulong JobProfileGetNextSerialNumber(IJobProfile jobProfile)
        {
            using (TimedLock tl = TimedLock.Lock(_jobProfileTable.TableLock))
            {
                JobProfileDataRow jobProfileDataRow = _jobProfileTable.Select(jobProfile.Id) ?? throw new InvalidOperationException();
                jobProfileDataRow.SerialNumber++;
                _jobProfileTable.Update(jobProfileDataRow);
                return jobProfileDataRow.SerialNumber;
            }
        }

        #endregion Job Profiles

        #region Tool Profiles

        public IReadOnlyList<IToolProfile> ToolsGet()
        {
            List<IToolProfile> Result = [];

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

        public void ToolAdd(IToolProfile toolProfile)
        {
            ArgumentNullException.ThrowIfNull(toolProfile);

            _toolDatabaseTable.Insert(new ToolDatabaseDataRow() 
            { 
                ToolName = toolProfile.Name, 
                Description = toolProfile.Description,
                LengthInMillimetres = toolProfile.LengthInMillimetres
            });
        }

        public void ToolUpdate(IToolProfile toolProfile)
        {
            ArgumentNullException.ThrowIfNull(toolProfile);

            ToolDatabaseDataRow dataRow = _toolDatabaseTable.Select(toolProfile.Id) ?? throw new InvalidOperationException("Tool not found");
            dataRow.ToolName = toolProfile.Name;
            dataRow.Description = toolProfile.Description;
            dataRow.LengthInMillimetres = toolProfile.LengthInMillimetres;
            dataRow.ExpectedLifeMinutes = toolProfile.ExpectedLifeMinutes;
            _toolDatabaseTable.Update(dataRow);
        }

        public void ToolResetUsage(IToolProfile toolProfile)
        {
            ArgumentNullException.ThrowIfNull(toolProfile);

            _memoryCache.GetExtendingCache().Clear();
            ToolDatabaseDataRow dataRow = _toolDatabaseTable.Select(toolProfile.Id) ?? throw new InvalidOperationException("Tool not found");
            dataRow.UsageLastReset = DateTime.UtcNow;
            double usage = JobExecutionByTool(toolProfile).TotalMinutes;

            dataRow.ToolHistory.Add(new ToolUsageHistory(dataRow.UsageLastReset, usage));
            _toolDatabaseTable.Update(dataRow);
        }

        #endregion Tool Profiles

        #region Services

        public void ServiceAdd(MachineServiceModel service)
        {
            MachineServiceDataRow serviceTableDataRow = new()
            {
                MachineId = service.MachineId,
                ServiceDate = DateTime.UtcNow,
                ServiceType = service.ServiceType,
                SpindleHours = service.SpindleHours,
            };

            foreach (KeyValuePair<long, string> kvp in service.ServiceItems)
                serviceTableDataRow.Items.Add(kvp.Key);

            _machineServiceTable.Insert(serviceTableDataRow);
        }

        public IReadOnlyList<MachineServiceModel> ServicesGet(long machineId)
        {
            List<MachineServiceModel> Result = [];

            foreach (var service in _machineServiceTable.Select(m => m.MachineId == machineId).OrderByDescending(m => m.ServiceDate))
            {
                Dictionary<long, string> serviceItems = [];

                foreach (long serviceItem in service.Items)
                    serviceItems.Add(serviceItem, _serviceItemsTable.Select(serviceItem).Name);

                Result.Add(new MachineServiceModel(service.Id, service.MachineId, service.ServiceDate, 
                    service.ServiceType, service.SpindleHours, serviceItems));
            }

            return Result;
        }

        public List<ServiceItemModel> ServiceItemsGet(MachineType machineType)
        {
            List<ServiceItemModel> serviceItems = [];

            foreach (var serviceItem in _serviceItemsTable.Select().Where(si => !si.IsDeleted))
            {
                ServiceItemModel serviceItemModel = new()
                {
                    Id = serviceItem.Id,
                    Name = serviceItem.Name,
                    IsDaily = serviceItem.IsDaily,
                    IsMinor = serviceItem.IsMinor,
                    IsMajor = serviceItem.IsMajor,
                };

                serviceItems.Add(serviceItemModel);
            }

            return serviceItems;
        }

        #endregion Services

        #region Private Methods

        private static IToolProfile CreateToolDatabaseModelFromToolDatabaseDataRow(ToolDatabaseDataRow toolDatabaseDataRow)
        {
            if (toolDatabaseDataRow == null)
                return null;

            List<ToolUsageHistoryModel> history = [];

            toolDatabaseDataRow.ToolHistory.ForEach(th => history.Add(new ToolUsageHistoryModel(th.LastChanged, th.UsageMinutes)));

            return new ToolProfileModel(history)
            {
                Id = toolDatabaseDataRow.Id,
                Name = toolDatabaseDataRow.ToolName,
                Description = toolDatabaseDataRow.Description,
                UsageLastReset = toolDatabaseDataRow.UsageLastReset,
                ExpectedLifeMinutes = toolDatabaseDataRow.ExpectedLifeMinutes,
                LengthInMillimetres = toolDatabaseDataRow.LengthInMillimetres,
            };
        }

        private MachineDataRow ConvertFromIMachineToMachineDataRow(IMachine machine)
        {
            if (machine == null)
                return null;

            MachineDataRow machineDataRow = _machineDataRow.Select(machine.Id) ?? new MachineDataRow();
            machineDataRow.Name = machine.Name;
            machineDataRow.MachineType = machine.MachineType;
            machineDataRow.MachineFirmware = machine.MachineFirmware;
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
            machineDataRow.Settings.CopyFrom(machine.Settings);

            return machineDataRow;
        }

        private List<IMachine> InternalGetMachines()
        {
            List<IMachine> Result = [];

            foreach (MachineDataRow machineDataRow in _machineDataRow.Select())
            {
                if (!machineDataRow.IsDeleted)
                    Result.Add(ConvertFromMachineDataRow(machineDataRow));
            }

            return Result;
        }

        private static IMachine ConvertFromMachineDataRow(MachineDataRow machineDataRow)
        {
            if (machineDataRow == null)
                return null;

            return new MachineModel(machineDataRow.Id, machineDataRow.Name, machineDataRow.MachineType,
                machineDataRow.MachineFirmware, machineDataRow.ComPort, machineDataRow.Options,
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
