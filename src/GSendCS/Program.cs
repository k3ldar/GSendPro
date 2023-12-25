using CommandLinePlus;

using GSendCS.Internal;
using GSendCS.Processors;

namespace GSendCS
{
    internal static class Program
    {
        static int Main()
        {
            ConsoleProcessorFactory factory = new();
            Environment.SetEnvironmentVariable(GSendShared.Constants.GSendPathEnvVar,
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                GSendShared.Constants.GSendProAppFolder));

            string pluginFileName = Path.Combine(Environment.GetEnvironmentVariable(GSendShared.Constants.GSendPathEnvVar),
                Internal.Constants.PluginFileName);
            string serverAddressFileName = Path.Combine(Environment.GetEnvironmentVariable(GSendShared.Constants.GSendPathEnvVar),
                Internal.Constants.ServerAddressFileName);

            object[] processors = new object[]
            {
                new PluginProcessor(pluginFileName),
                new ServerAddressProcessor(serverAddressFileName),
            };

            IConsoleProcessor consoleProcessor = factory.Create(GSend.Language.Resources.GSendPro, processors);

            switch (consoleProcessor.Run(new CommandLineOptions(), out int Result))
            {
                case RunResult.CandidateFound:
                    if (Result < 0)
                        WriteStandardResponse(Result);

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

        private static void WriteStandardResponse(int result)
        {
            switch (result)
            {
                case Internal.Constants.ResponseExclusiveAccessDenied:
                    Console.WriteLine(Properties.Resources.ExclusiveAccessDenied);
                    break;

                case Internal.Constants.ResponseExists:
                    Console.WriteLine(Properties.Resources.Exists);
                    break;

                case Internal.Constants.ResponseDoesNotExist:
                    Console.WriteLine(Properties.Resources.DoesNotExist);
                    break;

                case Internal.Constants.ResponseInvalidAddress:
                    Console.WriteLine(Properties.Resources.InvalidAddress);
                    break;

                case Internal.Constants.ResponseNoAddressesFound:
                    Console.WriteLine(Properties.Resources.NoAddressFound);
                    break;
            }
        }
    }
}