using System.IO.Ports;

using GSendShared.Abstractions;
using GSendShared.Models;

using static GSend.Language.Resources;
using static GSendShared.Constants;

namespace GSendShared.Helpers
{
    public static class ValidateParameters
    {
        public static IComPortModel ExtractComPortProperties(string[] values)
        {
            string portName = String.Empty;
            int timeOut = 1000;
            int baudRate = 115200;
            Parity parity = Parity.None;
            int dataBits = 8;
            StopBits stopBits = StopBits.One;

            if (values.Length > 0)
                portName = values[0];

            if (String.IsNullOrEmpty(portName))
                throw new ArgumentException(AnalyseError18);

            if (values.Length > 1)
            {
                if (!Int32.TryParse(values[1], out timeOut) || timeOut < MCodeMinTimeoutValue || timeOut > MCodeMaxTimeoutValue)
                    throw new ArgumentException(String.Format(AnalyseError13, ParameterTimeoutValue, ParameterNumber, MCodeMinTimeoutValue, MCodeMaxTimeoutValue));
            }

            if (values.Length > 2 && !Int32.TryParse(values[2], out baudRate))
            {
                throw new ArgumentException(String.Format(AnalyseError14, ParameterBaudRate, ParameterNumber));
            }

            if (baudRate < 1)
                throw new ArgumentException(AnalyseError19);

            if (values.Length > 3)
            {
                if (!Enum.TryParse(typeof(Parity), values[3], true, out object newParity))
                    throw new ArgumentException(AnalyseError15);

                parity = (Parity)newParity;
            }

            if (values.Length > 4)
            {
                if (!Int32.TryParse(values[4], out dataBits))
                    throw new ArgumentException(AnalyseError16);

                switch (dataBits)
                {
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        break;

                    default:
                        throw new ArgumentException(AnalyseError16);
                }
            }

            if (values.Length > 5)
            {
                if (!Enum.TryParse(typeof(StopBits), values[5], true, out object newStopBits))
                    throw new ArgumentException(AnalyseError17);

                stopBits = (StopBits)newStopBits;
            }

            return new ComPortModel(portName, timeOut, baudRate, parity, dataBits, stopBits);
        }

        public static M623Model ExtractM623Properties(IGCodeCommand command)
        {
            string commentStripped = command.CommentStripped(true);
            string[] values = commentStripped.Split(Constants.CharColon, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (values.Length == 0)
            {
                throw new ArgumentException(String.Format(AnalyseError8, command.Command, command.CommandValueString, command.LineNumber));
            }

            if (values.Length == 1)
            {
                throw new ArgumentException(String.Format(AnalyseError11, command.Command, command.CommandValueString, command.LineNumber));
            }
            else if (values.Length == 2)
            {
                throw new ArgumentException(String.Format(AnalyseError10, command.Command, command.CommandValueString, command.LineNumber));
            }
            else if (values.Length == 3)
            {
                throw new ArgumentException(String.Format(AnalyseError9, command.Command, command.CommandValueString, command.LineNumber));
            }

            if (!Int32.TryParse(values[1], out int timeoutPeriod) ||
                timeoutPeriod < Constants.MCodeMinTimeoutValue ||
                timeoutPeriod > Constants.MCodeMaxTimeoutValue)
            {
                throw new ArgumentException(String.Format(AnalyseError12, command.Command, command.CommandValueString, command.LineNumber,
                    Constants.MCodeMinTimeoutValue, Constants.MCodeMaxTimeoutValue));
            }

            string commandToSend = values[3];

            for (int i = 4; i < values.Length; i++)
            {
                commandToSend += Constants.ColonChar + values[i];
            }

            return new M623Model(values[0], values[2], timeoutPeriod, commandToSend);
        }
    }
}
