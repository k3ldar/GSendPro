using System.IO.Ports;

using GSendShared.Abstractions;
using GSendShared.Models;

using static GSend.Language.Resources;
using static GSendShared.Constants;

namespace GSendShared.Helpers
{
    public static class ValidateParameters
    {
        public static IComPortModel ExtractComPortProperties(string[] values, int timeOut)
        {
            string portName = String.Empty;
            int baudRate = 115200;
            Parity parity = Parity.None;
            int dataBits = 8;
            StopBits stopBits = StopBits.One;

            if (values.Length > 0)
                portName = values[0];

            if (String.IsNullOrEmpty(portName))
                throw new ArgumentException(AnalyzeError18);

            if (values.Length > 1 && !Int32.TryParse(values[1], out baudRate))
            {
                throw new ArgumentException(String.Format(AnalyzeError14, ParameterBaudRate, ParameterNumber));
            }

            if (baudRate < 1)
                throw new ArgumentException(AnalyzeError19);

            if (values.Length > 2)
            {
                if (!Enum.TryParse(typeof(Parity), values[2], true, out object newParity))
                    throw new ArgumentException(AnalyzeError15);

                parity = (Parity)newParity;
            }

            if (values.Length > 3)
            {
                if (!Int32.TryParse(values[3], out dataBits))
                    throw new ArgumentException(AnalyzeError16);

                switch (dataBits)
                {
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        break;

                    default:
                        throw new ArgumentException(AnalyzeError16);
                }
            }

            if (values.Length > 4)
            {
                if (!Enum.TryParse(typeof(StopBits), values[4], true, out object newStopBits))
                    throw new ArgumentException(AnalyzeError17);

                stopBits = (StopBits)newStopBits;
            }

            return new ComPortModel(portName, timeOut, baudRate, parity, dataBits, stopBits);
        }

        public static M623Model ExtractM623Properties(IGCodeCommand command, int timeOut)
        {
            string commentStripped = command.CommentStripped(true);
            string[] values = commentStripped.Split(Constants.CharColon, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (values.Length == 0)
            {
                throw new ArgumentException(String.Format(AnalyzeError8, command.Command, command.CommandValueString, command.LineNumber));
            }

            if (values.Length == 1)
            {
                throw new ArgumentException(String.Format(AnalyzeError10, command.Command, command.CommandValueString, command.LineNumber));
            }
            else if (values.Length == 2)
            {
                throw new ArgumentException(String.Format(AnalyzeError9, command.Command, command.CommandValueString, command.LineNumber));
            }

            string commandToSend = values[2];

            for (int i = 3; i < values.Length; i++)
            {
                commandToSend += Constants.ColonChar + values[i];
            }

            return new M623Model(values[0], values[1], timeOut, commandToSend);
        }
    }
}
