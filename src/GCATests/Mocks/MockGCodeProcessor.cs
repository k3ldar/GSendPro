using System;
using System.Collections.Generic;

using GSendShared;
using GSendShared.Models;

namespace GSendTests.Mocks
{
    internal class MockGCodeProcessor : IGCodeProcessor
    {
        public OverrideModel MachineOverrides { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public long Id => throw new NotImplementedException();

        public TimeSpan TimeOut { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan HomingTimeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Cpu => throw new NotImplementedException();

        public bool IsRunning => throw new NotImplementedException();

        public bool IsPaused => throw new NotImplementedException();

        public bool IsConnected => throw new NotImplementedException();

        public bool IsLocked => throw new NotImplementedException();

        public int CommandCount => throw new NotImplementedException();

        public int LineCount => throw new NotImplementedException();

        public int NextCommand => throw new NotImplementedException();

        public int BufferSize => throw new NotImplementedException();

        public bool SpindleActive => throw new NotImplementedException();

        public int SpindleSpeed => throw new NotImplementedException();

        public bool MistCoolantActive => throw new NotImplementedException();

        public bool FloodCoolantActive => throw new NotImplementedException();

        public MachineStateModel StateModel => throw new NotImplementedException();

        public IMachine Machine => throw new NotImplementedException();

#pragma warning disable CS0067
        public event GSendEventHandler OnConnect;
        public event GSendEventHandler OnDisconnect;
        public event GSendEventHandler OnStart;
        public event GSendEventHandler OnStop;
        public event GSendEventHandler OnPause;
        public event GSendEventHandler OnResume;
        public event CommandSentHandler OnCommandSent;
        public event GSendEventHandler OnSerialError;
        public event GSendEventHandler OnSerialPinChanged;
        public event GrblErrorHandler OnGrblError;
        public event GrblAlarmHandler OnGrblAlarm;
        public event GSendEventHandler OnInvalidComPort;
        public event GSendEventHandler OnComPortTimeOut;
        public event MachineStateHandler OnMachineStateChanged;
        public event MessageHandler OnMessageReceived;
        public event ResponseReceivedHandler OnResponseReceived;
        public event UpdateLineStatus OnLineStatusUpdated;
#pragma warning restore CS0067
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public ConnectResult Connect()
        {
            throw new NotImplementedException();
        }

        public bool CoolantOff()
        {
            throw new NotImplementedException();
        }

        public bool Disconnect()
        {
            throw new NotImplementedException();
        }

        public string Help()
        {
            throw new NotImplementedException();
        }

        public bool Home()
        {
            throw new NotImplementedException();
        }

        public bool JogStart(JogDirection jogDirection, double stepSize, double feedRate)
        {
            throw new NotImplementedException();
        }

        public bool JogStop()
        {
            throw new NotImplementedException();
        }

        public bool LoadGCode(IGCodeAnalyses gCodeAnalyses)
        {
            throw new NotImplementedException();
        }

        public bool Pause()
        {
            throw new NotImplementedException();
        }

        public bool Probe()
        {
            throw new NotImplementedException();
        }

        public void QueueCommand(string commandText)
        {
            throw new NotImplementedException();
        }

        public bool Resume()
        {
            throw new NotImplementedException();
        }

        public string SendCommandWaitForOKCommand(string commandText)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, object> Settings()
        {
            throw new NotImplementedException();
        }

        public bool Start(IJobExecution jobExecution)
        {
            throw new NotImplementedException();
        }

        public bool Stop()
        {
            throw new NotImplementedException();
        }

        public bool ToggleSimulation()
        {
            throw new NotImplementedException();
        }

        public bool TurnFloodCoolantOn()
        {
            throw new NotImplementedException();
        }

        public bool TurnMistCoolantOn()
        {
            throw new NotImplementedException();
        }

        public bool Unlock()
        {
            throw new NotImplementedException();
        }

        public UpdateSettingResult UpdateSetting(string updateCommand)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSpindleSpeed(int speed, bool clockWise)
        {
            throw new NotImplementedException();
        }

        public bool WriteLine(string gCode)
        {
            throw new NotImplementedException();
        }

        public bool ZeroAxes(ZeroAxis axis, int coordinateSystem)
        {
            throw new NotImplementedException();
        }
    }
}
