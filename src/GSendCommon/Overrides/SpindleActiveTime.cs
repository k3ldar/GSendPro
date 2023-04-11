using GSendDB.Tables;

using GSendShared;
using GSendShared.Interfaces;

using SimpleDB;

namespace GSendCommon.Overrides
{
    /// <summary>
    /// Logs spindle active time
    /// </summary>
    public sealed class SpindleActiveTime : IGCodeOverride
    {
        private MachineSpindleTimeDataRow _activeRow = null;
        private readonly ISimpleDBOperations<MachineSpindleTimeDataRow> _spindleTimeTable;

        public SpindleActiveTime(ISimpleDBOperations<MachineSpindleTimeDataRow> spindleTimeTable)
        {
            _spindleTimeTable = spindleTimeTable ?? throw new ArgumentNullException(nameof(_spindleTimeTable));
        }

        public int SortOrder => int.MinValue;

        public void Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            IGCodeCommand startStopCommand = overrideContext.GCode.Commands.FirstOrDefault(c => c.Command.Equals('M') && 
                (
                    c.CommandValue.Equals(2) || 
                    c.CommandValue.Equals(3) || 
                    c.CommandValue.Equals(4) || 
                    c.CommandValue.Equals(5) || 
                    c.CommandValue.Equals(30)
                ));

            if (startStopCommand != null)
            {
                if (startStopCommand.CommandValue.Equals(3) || startStopCommand.CommandValue.Equals(4))
                {
                    IGCodeCommand spindleSpeed = overrideContext.GCode.Commands.FirstOrDefault(c => c.Command.Equals('S'));

                    if (_activeRow != null)
                    {
                        _activeRow.FinishTime = DateTime.UtcNow;
                        _spindleTimeTable.Update(_activeRow);
                    }

                    _activeRow = new MachineSpindleTimeDataRow()
                    {
                        StartTime = DateTime.UtcNow,
                        FinishTime = DateTime.MinValue,
                        MachineId = overrideContext.Machine.Id,
                        MaxRpm = spindleSpeed == null ? 0 : (int)spindleSpeed.CommandValue,
                    };

                    _spindleTimeTable.Insert(_activeRow);
                }
                else
                {
                    StopSpindleTimer();
                }
            }
        }

        public void Process(GrblAlarm alarm)
        {
            StopSpindleTimer();
        }

        public void Process(GrblError error)
        {
            StopSpindleTimer();
        }

        private void StopSpindleTimer()
        {
            if (_activeRow != null)
            {
                _activeRow.FinishTime = DateTime.UtcNow;
                _spindleTimeTable.Update(_activeRow);
                _activeRow = null;
            }
        }
    }
}
