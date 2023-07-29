using System.Diagnostics;

using GSendShared;
using GSendShared.Abstractions;

namespace GSendCommon.MCodeOverrides
{
    internal class M630Override : IMCodeOverride
    {
        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            List<IGCodeCommand> m630Commands = overrideContext.GCode.Commands.Where(c => c.Command.Equals(Constants.CharM) && c.CommandValue.Equals(Constants.MCode630RunProgram)).ToList();

            if (m630Commands.Count == 0)
                return false;

            if (m630Commands.Count == 1)
            {
                IGCodeCommand command = m630Commands[0];
                string comment = command.CommentStripped(true);

                try
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(comment);

                    //run the exe here after checking for M630.1 in previous line
                    if (command.PreviousCommand != null && command.PreviousCommand.Command.Equals(Constants.CharM) && command.PreviousCommand.CommandValue.Equals(Constants.MCode630RunProgramParams))
                    {
                        processStartInfo.Arguments = command.PreviousCommand.CommentStripped(true);
                    }

                    processStartInfo.UseShellExecute = true;

                    System.Diagnostics.Process.Start(processStartInfo);

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
