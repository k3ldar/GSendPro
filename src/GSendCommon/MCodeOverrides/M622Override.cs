using GSendShared;
using GSendShared.Abstractions;

namespace GSendCommon.MCodeOverrides
{
    internal class M622Override : IMCodeOverride
    {
        private readonly IComPortFactory _comPortFactory;

        public M622Override(IComPortFactory comPortFactory)
        {
            _comPortFactory = comPortFactory ?? throw new ArgumentNullException(nameof(comPortFactory));
        }

        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            List<IGCodeCommand> m622Commands = overrideContext.GCode.Commands.Where(c => c.Command.Equals(Constants.CharM) && c.CommandValue.Equals(Constants.MCode622)).ToList();

            if (m622Commands.Count == 0)
                return false;

            if (m622Commands.Count == 1)
            {
                IGCodeCommand command = m622Commands[0];
                string comment = command.CommentStripped(true);
                string[] comPortComments = comment.Split(Constants.CharColon, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    if (comPortComments.Length == 0)
                        throw new ArgumentException(String.Format(GSend.Language.Resources.AnalyseError8, command.Command, command.CommandValueString, command.LineNumber));

                    if (comPortComments.Length == 1)
                        throw new ArgumentException(String.Format(GSend.Language.Resources.AnalyseError9, command.Command, command.CommandValueString, command.LineNumber));

                    IComPort comPort = _comPortFactory.GetComPort(comPortComments[0]);

                    if (!comPort.IsOpen())
                        throw new InvalidOperationException(String.Format(GSend.Language.Resources.ComPortClosed, comPort.Name));

                    string dataToSend = comment.Substring(comPortComments[0].Length + 1);

                    comPort.WriteLine(dataToSend);
                    overrideContext.SendInformationUpdate(InformationType.Information, String.Format(GSend.Language.Resources.ComPortDataSent, dataToSend, comPort.Name));

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
    }
}
