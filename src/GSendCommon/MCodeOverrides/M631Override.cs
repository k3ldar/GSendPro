using GSendShared;
using GSendShared.Abstractions;
using Shared.Classes;
using System.Diagnostics;

namespace GSendCommon.MCodeOverrides
{
    internal class M631Override : BaseOverride, IMCodeOverride
    {
        private readonly IRunProgram _runProgram;

        public M631Override(IRunProgram runProgram)
        {
            _runProgram = runProgram;
        }

        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            List<IGCodeCommand> mCommands = overrideContext.GCode.Commands.Where(c => c.Command.Equals(Constants.CharM) && c.CommandValue.Equals(Constants.MCode631RunProgram)).ToList();

            if (mCommands.Count == 0)
                return false;

            if (mCommands.Count == 1)
            {
                IGCodeCommand command = mCommands[0];
                string program = command.CommentStripped(true);

                try
                {
                    List<IGCodeCommand> previousCommands = PreviousCommands(command, new string[] { "M601", "M631.1", "M631.2", "P" });

                    IGCodeCommand parameters = previousCommands.Find(p => p.CommandValue == Constants.MCode631RunProgramParams);
                    string args = null;
                                        
                    if (parameters != null)
                    {
                        args = parameters.CommentStripped(true);
                    }

                    int returnCode = 0;
                    IGCodeCommand returnResult = previousCommands.Find(p => p.CommandValue == Constants.MCode631RunProgramResult);

                    if (GetAdjoiningGCodeCommandValue(returnResult, Constants.CharP, out decimal defaultValue))
                        returnCode = Convert.ToInt32(defaultValue);
                    else
                        overrideContext.SendInformationUpdate(InformationType.Warning, 
                            String.Format(GSend.Language.Resources.AnaylsesWarningNoReturnCode, mCommands[0].MasterLineNumber));

                    int timeoutMilliseconds = 1000;

                    IGCodeCommand timeoutResult = previousCommands.Find(p => p.CommandValue == Constants.MCode601Timeout);
                    if (GetAdjoiningGCodeCommandValue(timeoutResult, Constants.CharP, out decimal defaultTimeout))
                        timeoutMilliseconds = Convert.ToInt32(defaultTimeout);
                    else
                        overrideContext.SendInformationUpdate(InformationType.Warning,
                            String.Format(GSend.Language.Resources.AnalysesWarningDefaultTimeoutRunProgram, mCommands[0].MasterLineNumber));

                    int runResult = _runProgram.Run(program, args, false, true, timeoutMilliseconds);

                    if (runResult != returnCode)
                    {
                        overrideContext.SendInformationUpdate(InformationType.Error, 
                            String.Format(GSend.Language.Resources.AnalyzeError37, mCommands[0].MasterLineNumber, returnCode, runResult));
                    }

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
