using System.Text;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Helpers;
using GSendShared.Models;

namespace GSendCommon.MCodeOverrides
{
    internal class M623Override : IMCodeOverride
    {
        private readonly IComPortFactory _comPortFactory;

        public M623Override(IComPortFactory comPortFactory)
        {
            _comPortFactory = comPortFactory ?? throw new ArgumentNullException(nameof(comPortFactory));
        }

        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            List<IGCodeCommand> m623Commands = overrideContext.GCode.Commands.Where(c => c.Command.Equals(Constants.CharM) && c.CommandValue.Equals(Constants.MCode623)).ToList();

            if (m623Commands.Count == 0)
                return false;

            if (m623Commands.Count == 1)
            {
                IGCodeCommand command = m623Commands[0];
                string comment = command.CommentStripped(true);

                try
                {
                    M623Model m623Model = ValidateParameters.ExtractM623Properties(command, overrideContext.Variables[Constants.SystemVariableTimeout].IntValue);

                    IComPort comPort = _comPortFactory.GetComPort(m623Model.ComPort);

                    if (!comPort.IsOpen())
                        throw new InvalidOperationException(String.Format(GSend.Language.Resources.ComPortClosed, comPort.Name));

                    string response = SendCommandWaitForCorrectResponse(comPort, m623Model);

                    overrideContext.SendInformationUpdate(InformationType.Information, String.Format(GSend.Language.Resources.ComPortDataSentResponseReceived, m623Model.Command, comPort.Name, m623Model.Response));

                    overrideContext.SendCommand = false;
                    return true;
                }
                catch (Exception ae)
                {
                    overrideContext.SendInformationUpdate(InformationType.Error, ae.Message);
                    overrideContext.ProcessError(ae);
                    throw;
                }
            }

            return false;
        }

        public string SendCommandWaitForCorrectResponse(IComPort comPort, M623Model m623Model)
        {
            StringBuilder Result = new(1024);

            _ = comPort.ReadLine();
            comPort.WriteLine(m623Model.Command);
            DateTime sendTime = DateTime.UtcNow;
            TimeSpan timeOut = TimeSpan.FromMilliseconds(m623Model.Timeout);

            while (true)
            {
                string line = comPort.ReadLine();

                if (line.Trim().Equals(m623Model.Response))
                    break;

                if (!String.IsNullOrEmpty(line.Trim()))
                    Result.AppendLine(line);


                if (DateTime.UtcNow - sendTime > timeOut)
                    throw new TimeoutException();

                Thread.Sleep(TimeSpan.Zero);
            }

            return Result.ToString();
        }

    }
}
