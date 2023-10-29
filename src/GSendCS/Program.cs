using System.Net;

using CommandLinePlus;

using GSendCS.Internal;
using GSendCS.Processors;

namespace GSendCS
{
    internal static class Program
    {
        static int Main()
        {
            IConsoleProcessorFactory factory = new ConsoleProcessorFactory();
            Environment.SetEnvironmentVariable(GSendShared.Constants.GSendPathEnvVar,
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), 
                GSendShared.Constants.GSendProAppFolder));

            string pluginFileName = Path.Combine(Environment.GetEnvironmentVariable(GSendShared.Constants.GSendPathEnvVar),
                Internal.Constants.PluginFileName);
            object[] processors = new object[]
            {
                new PluginProcessor(pluginFileName),
            };

            IConsoleProcessor consoleProcessor = factory.Create(GSend.Language.Resources.GSendPro, processors);

            switch (consoleProcessor.Run(new CommandLineOptions(), out int Result))
            {
                case RunResult.CandidateFound:
                    if (Result < 10000)
                        Console.WriteLine(GSend.Language.Resources.ConsoleComplete);

                    break;

                case RunResult.DisplayHelp:
                    break;

                default:
                    Console.WriteLine(GSend.Language.Resources.ConsoleInvalidCmdLine);
                    Result = -1;
                    break;
            }

            foreach (var item in processors)
            {
                if (item is IDisposable disposableProcessor)
                    disposableProcessor.Dispose();
            }

            if (Result == Int32.MaxValue)
                Result = 0;

            return Result;
        }
    }
}