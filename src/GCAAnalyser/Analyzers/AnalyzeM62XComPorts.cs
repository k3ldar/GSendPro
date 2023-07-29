using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Abstractions;
using GSendShared;
using GSendShared.Helpers;

namespace GSendAnalyzer.Analyzers
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
            List<IGCodeCommand> comPortCommands = gCodeAnalyses.AllSpecificCommands(Constants.CharM).Where(c => IsComPortCommand(c.CommandValue)).ToList();

            if (comPortCommands.Count == 0)
                return;

            if (gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                Dictionary<string, bool> comPortUsage = new();

                foreach (IGCodeCommand command in comPortCommands)
                {
                    string[] comPortComments = command.CommentStripped(true).Split(Constants.CharColon, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                    if (comPortComments.Length == 0)
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalyzeError8, command.Command, command.CommandValueString, command.LineNumber);
                        continue;
                    }

                    string comPort = comPortComments[0];

                    if (!_comPortProvider.AvailablePorts().Contains(comPort))
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalysesError7, comPort, command.LineNumber);
                    }
                    else if (command.CommandValue == Constants.MCode620)
                    {
                        try
                        {
                            _ = ValidateParameters.ExtractComPortProperties(comPortComments, gCodeAnalyses.Variables[Constants.SystemVariableTimeout].IntValue);
                        }
                        catch (ArgumentException ae)
                        {
                            codeAnalyses.AddError(ae.Message);
                            continue;
                        }

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
                    else if (command.CommandValue == Constants.MCode621)
                    {
                        // close port
                        if (!comPortUsage.ContainsKey(comPort))
                            codeAnalyses.AddError(GSend.Language.Resources.AnalysesError4, command.LineNumber, comPort);
                        else if (!comPortUsage[comPort])
                            codeAnalyses.AddError(GSend.Language.Resources.AnalysesError5, command.LineNumber, comPort);
                        else
                            comPortUsage[comPort] = false;
                    }
                    else if (command.CommandValue == Constants.MCode622)
                    {
                        if (comPortComments.Length == 1)
                        {
                            codeAnalyses.AddError(GSend.Language.Resources.AnalyzeError9, command.Command, command.CommandValueString, command.LineNumber);
                        }
                    }
                    else if (command.CommandValue == Constants.MCode623)
                    {
                        try
                        {
                            _ = ValidateParameters.ExtractM623Properties(command, codeAnalyses.Variables[Constants.SystemVariableTimeout].IntValue);
                        }
                        catch (ArgumentException ae)
                        {
                            codeAnalyses.AddError(ae.Message);
                            continue;
                        }
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
                case Constants.MCode620:
                case Constants.MCode621:
                case Constants.MCode622:
                case Constants.MCode623:

                    return true;
            }

            return false;
        }
    }
}
