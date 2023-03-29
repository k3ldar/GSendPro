﻿using GSendDB.Tables;

using GSendShared;
using GSendShared.Models;

using SimpleDB;

namespace GSendDB.Providers
{
    internal class MachineProvider : IMachineProvider
    {
        private readonly ISimpleDBOperations<MachineDataRow> _machineDataRow;

        public MachineProvider(ISimpleDBOperations<MachineDataRow> machineDataRow)
        {
            _machineDataRow = machineDataRow ?? throw new ArgumentNullException(nameof(machineDataRow));
        }

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

        public IMachine MachineGet(long id)
        {
            return ConvertFromMachineDataRow(_machineDataRow.Select(id));
        }

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
            machineDataRow.OverrideSpeed = machine.OverrideSpeed;
            machineDataRow.OverrideSpindle = machine.OverrideSpindle;
            machineDataRow.ConfigurationLastVerified = machine.ConfigurationLastVerified;
            machineDataRow.ProbeCommand = machine.ProbeCommand;
            machineDataRow.ProbeSpeed = machine.ProbeSpeed;
            machineDataRow.ProbeThickness = machine.ProbeThickness;
            machineDataRow.JogFeedRate = machine.JogFeedrate;
            machineDataRow.JogUnits = machine.JogUnits;
            machineDataRow.SpindleType = machine.SpindleType;
            machineDataRow.SoftStart = machine.SoftStart;
            machineDataRow.SoftStartSeconds = machine.SoftStartSeconds;

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
                machineDataRow.DisplayUnits, machineDataRow.OverrideSpeed,
                machineDataRow.OverrideSpindle, machineDataRow.ConfigurationLastVerified,
                machineDataRow.ProbeCommand, machineDataRow.ProbeSpeed, machineDataRow.ProbeThickness,
                machineDataRow.JogFeedRate, machineDataRow.JogUnits,
                machineDataRow.SpindleType, machineDataRow.SoftStartSeconds, machineDataRow.SoftStart);
        }

        #endregion Private Methods
    }
}
