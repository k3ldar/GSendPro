using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Abstractions;
using GSendShared;

namespace GSendCommon.MCodeOverrides
{
    internal class M630Override : IMCodeOverride
    {
        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            List<IGCodeCommand> m630Commands = overrideContext.GCode.Commands.Where(c => c.Command.Equals(Constants.CharM) && c.CommandValue.Equals(Constants.MCode630)).ToList();

            if (m630Commands.Count == 0)
                return false;

            if (m630Commands.Count == 1)
            {
                IGCodeCommand command = m630Commands[0];
                string comment = command.CommentStripped(true);

                try
                {
                    //overrideContext.SendInformationUpdate(InformationType.Information, String.Format(GSend.Language.Resources.ComPortDataSent, dataToSend, comPort.Name));

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
