using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Abstractions;
using GSendShared.Helpers;
using GSendShared;

namespace GSendCommon.MCodeOverrides
{
    internal class M621Override : IMCodeOverride
    {
        private readonly IComPortFactory _comPortFactory;

        public M621Override(IComPortFactory comPortFactory)
        {
            _comPortFactory = comPortFactory ?? throw new ArgumentNullException(nameof(comPortFactory));
        }

        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            List<IGCodeCommand> m621Commands = overrideContext.GCode.Commands.Where(c => c.Command.Equals(Constants.CharM) && c.CommandValue.Equals(Constants.MCode621)).ToList();

            if (m621Commands.Count == 0)
                return false;

            if (m621Commands.Count == 1)
            {
                IGCodeCommand command = m621Commands[0];
                string[] comPortComments = command.CommentStripped(true).Split(Constants.CharColon, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    if (comPortComments.Length == 0)
                        throw new ArgumentException(String.Format(GSend.Language.Resources.AnalyseError8, command.Command, command.CommandValueString, command.LineNumber));

                    IComPort comPort = _comPortFactory.GetComPort(comPortComments[0]);
                    _comPortFactory.DeleteComPort(comPort);
                    overrideContext.SendInformationUpdate(InformationType.Information, String.Format(GSend.Language.Resources.ComPortClosed, comPort.Name));

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
