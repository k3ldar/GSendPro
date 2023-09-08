using System.Diagnostics;

using GSendShared;
using GSendShared.Abstractions;
using Shared.Classes;

namespace GSendCommon.MCodeOverrides
{
    internal class M630Override : BaseOverride, IMCodeOverride
    {
        private readonly IRunProgram _runProgram;

        public M630Override(IRunProgram runProgram)
        {
            _runProgram = runProgram;
        }

        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            List<IGCodeCommand> m630Commands = overrideContext.GCode.Commands.Where(c => c.Command.Equals(Constants.CharM) && c.CommandValue.Equals(Constants.MCode630RunProgram)).ToList();

            if (m630Commands.Count == 0)
                return false;

            if (m630Commands.Count == 1)
            {
                IGCodeCommand command = m630Commands[0];
                string programName = command.CommentStripped(true);

                try
                {
                    string args = null;

                    //run the exe here after checking for M630.1 in previous line
                    if (command.PreviousCommand != null && command.PreviousCommand.Command.Equals(Constants.CharM) && command.PreviousCommand.CommandValue.Equals(Constants.MCode630RunProgramParams))
                    {
                        args = command.PreviousCommand.CommentStripped(true);
                    }

                    _runProgram.Run(programName, args, true, false, 0);

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
