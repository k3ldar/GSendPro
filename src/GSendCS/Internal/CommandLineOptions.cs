﻿using CommandLinePlus;

namespace GSendCS.Internal
{
    internal sealed class CommandLineOptions : ICommandLineOptions
    {
        public bool ShowVerbosity => true;

        public bool ShowHelpMessage => true;

        public string SubOptionPrefix => "  ";

        public int SubOptionMinimumLength => 20;

        public string SubOptionSuffix => "  ";

        public string ParameterPrefix => "   ";

        public int ParameterMinimumLength => 18;

        public string ParameterSuffix => "  ";

        public int InternalOptionsMinimumLength => 22;

        public bool CaseSensitiveOptionNames => true;

        public bool CaseSensitiveSubOptionNames => false;

        public bool CaseSensitiveParameterNames => false;
    }
}
