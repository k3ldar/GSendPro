﻿using GSendShared;
using GSendShared.Abstractions;

namespace GSendCommon.OverrideClasses
{
    /// <summary>
    /// Logs spindle active time
    /// </summary>
    public sealed class SpindleActiveTime : IGCodeOverride, IDisposable
    {
        private readonly IGSendDataProvider _gSendDataProvider;
        private long _spindleTimeId = -1;

        public SpindleActiveTime(IGSendDataProvider gSendDataProvider)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
        }

        ~SpindleActiveTime()
        {
            Dispose();
        }

        public MachineType MachineType => MachineType.CNC;

        public int SortOrder => int.MinValue;

        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            IGCodeCommand startStopCommand = overrideContext.GCode.Commands.Find(c => c.Command.Equals('M') &&
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
                    IGCodeCommand spindleSpeed = overrideContext.GCode.Commands.Find(c => c.Command.Equals('S'));

                    if (_spindleTimeId > -1)
                    {
                        _gSendDataProvider.SpindleTimeFinish(_spindleTimeId);
                    }

                    _spindleTimeId = _gSendDataProvider.SpindleTimeCreate(overrideContext.Machine.Id,
                        spindleSpeed == null ? 0 : (int)spindleSpeed.CommandValue,
                        overrideContext.JobExecution.ToolProfile.Id);
                }
                else
                {
                    StopSpindleTimer();
                }

                return false;
            }

            return false;
        }

        public void Process(GrblAlarm alarm)
        {
            StopSpindleTimer();
        }

        public void Process(GrblError error)
        {
            StopSpindleTimer();
        }

        public void Process(Exception error)
        {

        }

        private void StopSpindleTimer()
        {
            if (_spindleTimeId > -1)
            {
                _gSendDataProvider.SpindleTimeFinish(_spindleTimeId);
                _spindleTimeId = -1;
            }
        }

        public void Dispose()
        {
            StopSpindleTimer();
            GC.SuppressFinalize(this);
        }
    }
}
