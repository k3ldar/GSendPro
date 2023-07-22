using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;

using GSendShared;
using GSendShared.Abstractions;

namespace GSendTests.Mocks
{
    [ExcludeFromCodeCoverage]
    internal sealed class MockComPort : IComPort
    {
        private bool _isOpen = false;
        private readonly List<string> _commands = new();
        private string _activeRequest = "";

        private int _lastCommandId = -1;

        public MockComPort(IMachine machine)
        {
            Machine = machine ?? throw new ArgumentNullException(nameof(machine));
        }
        public MockComPort(IComPortModel comPortModel)
        {
            Model = comPortModel ?? throw new ArgumentNullException(nameof(comPortModel));
            Name = comPortModel.Name;
        }

        public IMachine Machine { get; private set; }

        public IComPortModel Model { get; private set; }

        public List<string> CommandsToReturn { get; set; } = new();


        public event SerialErrorReceivedEventHandler ErrorReceived;
        public event SerialPinChangedEventHandler PinChanged;
        public event EventHandler DataReceived;

        public void Close()
        {
            if (!_isOpen)
                throw new InvalidOperationException("Should not close when not open");

            _isOpen = false;
        }

        public bool IsOpen()
        {
            return _isOpen;
        }

        public void Open()
        {
            if (ThrowFileNotFoundException)
                throw new FileNotFoundException(Model == null ? Machine.ComPort : Model.Name);

            if (_isOpen)
                throw new InvalidOperationException("Should not open when already open");

            _isOpen = true;
        }

        public string ReadLine()
        {
            if (_activeRequest == "$")
            {
                _activeRequest = String.Empty;
                return "HLP:$$ $# $G $I $N $x=val $Nx=line $J=line $SLP $C $X $H ~ ! ? ctrl-x]";
            }
            else if (_activeRequest == "$I")
            {
                _lastCommandId++;

                switch (_lastCommandId)
                {
                    case 0:
                        return "[VER: 1.1f.20170802:]";
                    case 1:
                        return "[OPT: VNM, 35, 255]";
                    case 2:
                        return "ok";
                }
            }
            else if (_activeRequest == "$$")
            {
                _lastCommandId++;

                switch (_lastCommandId)
                {
                    case 0:
                        return $"${0}=10";
                    case 1:
                        return $"${1}=25";
                    case 2:
                        return $"${2}=0";
                    case 3:
                        return $"${3}=0";
                    case 4:
                        return $"${4}=0";
                    case 5:
                        return $"${5}=0";
                    case 6:
                        return $"${6}=0";
                    case 7:
                        return $"${10}=1";
                    case 8:
                        return $"${11}=0.010";
                    case 9:
                        return $"${12}=0.002";
                    case 10:
                        return $"${13}=0";
                    case 11:
                        return $"${20}=0";
                    case 12:
                        return $"${21}=1";
                    case 13:
                        return $"${22}=1";
                    case 14:
                        return $"${23}=0";
                    case 15:
                        return $"${24}=25.000";
                    case 16:
                        return $"${26}=250";
                    case 17:
                        return $"${27}=1.000";
                    case 18:
                        return $"${30}=1000";
                    case 19:
                        return $"${31}=0";
                    case 20:
                        return $"${32}=0";
                    case 21:
                        return $"${100}=250.000";
                    case 22:
                        return $"${101}=250.000";
                    case 23:
                        return $"${102}=250.000";
                    case 24:
                        return $"${110}=500.000";
                    case 25:
                        return $"${111}=500.000";
                    case 26:
                        return $"${112}=500.000";
                    case 27:
                        return $"${120}=10.000";
                    case 28:
                        return $"${121}=10.000";
                    case 29:
                        return $"${122}=10.000";
                    case 30:
                        return $"${130}=200.000";
                    case 31:
                        return $"${131}=200.000";
                    case 32:
                        _activeRequest = String.Empty;
                        return $"${132}=200.000";
                }
            }
            else if (CommandsToReturn.Count > 0 && _lastCommandId < CommandsToReturn.Count)
            {
                _lastCommandId++;

                return CommandsToReturn[_lastCommandId];

            }


            _lastCommandId = -1;
            _activeRequest = "";
            return "ok";
        }

        public void WriteLine(string line)
        {
            _activeRequest = line;
            _commands.Add(line);

            if (DelayResponse.TotalMilliseconds > 0)
            {
                Task.Run(() =>
                {
                    Task.Delay(DelayResponse);
                    DataReceived?.Invoke(this, EventArgs.Empty);
                });
            }
            else
            {
                DataReceived?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<string> Commands => _commands;

        public TimeSpan DelayResponse { get; set; } = TimeSpan.Zero;

        public bool ThrowFileNotFoundException { get; set; }

        public string Name { get; set; }

        public void RaisePinError()
        {
            PinChanged?.Invoke(this, null);
        }

        public void RaiseSerialError()
        {
            ErrorReceived?.Invoke(this, null);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            _commands.Add(Encoding.Latin1.GetString(buffer));
        }

        public bool CanReadLine()
        {
            return _lastCommandId != -1 || !String.IsNullOrEmpty(_activeRequest);
        }
    }
}
