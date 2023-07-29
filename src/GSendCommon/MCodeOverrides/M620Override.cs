using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Helpers;

namespace GSendCommon.MCodeOverrides
{
    internal class M620Override : IMCodeOverride
    {
        private readonly IComPortFactory _comPortFactory;

        public M620Override(IComPortFactory comPortFactory)
        {
            _comPortFactory = comPortFactory ?? throw new ArgumentNullException(nameof(comPortFactory));
        }

        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            List<IGCodeCommand> m620Commands = overrideContext.GCode.Commands.Where(c => c.Command.Equals(Constants.CharM) && c.CommandValue.Equals(Constants.MCode620)).ToList();

            if (m620Commands.Count == 0)
                return false;

            if (m620Commands.Count == 1)
            {
                IGCodeCommand command = m620Commands[0];
                string[] comPortComments = command.CommentStripped(true).Split(Constants.CharColon, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    IComPortModel comPortModel = ValidateParameters.ExtractComPortProperties(comPortComments, overrideContext.Variables[Constants.SystemVariableTimeout].IntValue);
                    IComPort comPort = _comPortFactory.CreateComPort(comPortModel);
                    overrideContext.SendInformationUpdate(InformationType.Information, String.Format(GSend.Language.Resources.ComPortOpened, comPort.Name));
                    overrideContext.SendCommand = false;
                    comPort.Open();
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
