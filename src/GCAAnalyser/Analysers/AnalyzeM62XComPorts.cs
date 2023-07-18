using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Abstractions;
using GSendShared;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeM62XComPorts : IGCodeAnalyzer
    {
        private readonly IComPortProvider _comPortProvider;

        public AnalyzeM62XComPorts(IComPortProvider comPortFactory)
        {
            _comPortProvider = comPortFactory ?? throw new ArgumentNullException(nameof(comPortFactory));
        }

        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> m620Commands = gCodeAnalyses.AllSpecificCommands(Constants.CharM).Where(c => IsComPortCommand(c.CommandValue)).ToList();

            if (m620Commands.Count == 0)
                return;

            if (gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                Dictionary<string, bool> comPortUsage = new();

                foreach (IGCodeCommand command in m620Commands)
                {
                    string comPort = command.CommentStripped(true);

                    if (!_comPortProvider.AvailablePorts().Contains(comPort))
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalysesError7, comPort, command.LineNumber);
                    }
                    else if (command.CommandValue == 620)
                    {
                        //open port
                        if (!comPortUsage.ContainsKey(comPort))
                        {
                            comPortUsage.Add(comPort, false);
                        }

                        if ((command.PreviousCommand?.LineNumber == command.LineNumber || command.NextCommand?.LineNumber == command.LineNumber) &&
                            (command.PreviousCommand?.MasterLineNumber == command.MasterLineNumber || command.NextCommand?.MasterLineNumber == command.MasterLineNumber))
                        {
                            codeAnalyses.AddError(GSend.Language.Resources.AnalysesError3, command.LineNumber);
                        }

                        if (comPortUsage[comPort])
                            codeAnalyses.AddError(GSend.Language.Resources.AnalysesError2, command.LineNumber, comPort);

                        comPortUsage[comPort] = true;
                    }
                    else if (command.CommandValue == 621)
                    {
                        // close port
                        if (!comPortUsage.ContainsKey(comPort))
                            codeAnalyses.AddError(GSend.Language.Resources.AnalysesError5, command.LineNumber, comPort);
                        else if (!comPortUsage[comPort])
                            codeAnalyses.AddError(GSend.Language.Resources.AnalysesError4, command.LineNumber, comPort);
                        else
                            comPortUsage[comPort] = false;
                    }
                }

                foreach (KeyValuePair<string, bool> kvp in comPortUsage)
                {
                    if (kvp.Value)
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalysesError6, kvp.Key);
                    }
                }
            }
        }

        private static bool IsComPortCommand(decimal value)
        {
            switch (value)
            {
                case 620:
                case 621:
                case 622:

                    return true;
            }

            return false;
        }
    }
}
