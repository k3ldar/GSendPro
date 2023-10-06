using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using GSendControls;

using GSendShared;

using Microsoft.Extensions.DependencyInjection;

using PluginManager;
using PluginManager.Abstractions;

using Shared.Classes;

namespace GSendDesktop
{
    internal static class Program
    {
        private static ILogger _logger;

        [STAThread]
        static void Main()
        {
            if (CheckForUpdate())
                return;

            ThreadManager.Initialise(new SharedLib.Win.WindowsCpuUsage());
            ThreadManager.AllowThreadPool = true;
            ThreadManager.MaximumPoolSize = 5000;
            ThreadManager.ThreadExceptionRaised += ThreadManager_ThreadExceptionRaised;
            ThreadManager.ThreadCancellAll += ThreadManager_ThreadCancellAll;
            ThreadManager.ThreadForcedToClose += ThreadManager_ThreadForcedToClose;
            ThreadManager.ThreadStopped += ThreadManager_ThreadStopped;

            Environment.SetEnvironmentVariable(Constants.GSendPathEnvVar,
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder));

            Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable(Constants.GSendPathEnvVar)));

            ApplicationPluginManager applicationPluginManager = new(
                new PluginManagerConfiguration(),
                new PluginSettings());

            applicationPluginManager.RegisterPlugin(typeof(ApplicationPluginManager).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(PluginSetting).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendAnalyzer.PluginInitialisation).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendCommon.PluginInitialisation).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendShared.PluginInitialisation).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendApi.PluginInitialization).Assembly.Location);

            IServiceCollection serviceCollection = new ServiceCollection();
            applicationPluginManager.ConfigureServices(serviceCollection);
            applicationPluginManager.ServiceProvider = serviceCollection.BuildServiceProvider();

            applicationPluginManager.LoadAllPlugins(GSendShared.Plugins.PluginHosts.Sender,
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder, Constants.AppPluginFile));

            _logger = applicationPluginManager.ServiceProvider.GetService<ILogger>();
            IGSendContext gSendContext = applicationPluginManager.ServiceProvider.GetService<IGSendContext>();
            try
            {
                GlobalExceptionHandler.InitializeGlobalExceptionHandlers(gSendContext.ServiceProvider.GetRequiredService<ILogger>());
                ApplicationConfiguration.Initialize();
                Application.Run(gSendContext.ServiceProvider.GetRequiredService<FormMain>());
            }
            finally
            {
                ThreadManager.CancelAll();
                gSendContext.CloseContext();
            }
        }

        private static void ThreadManager_ThreadStopped(object sender, Shared.ThreadManagerEventArgs e)
        {
            _logger.AddToLog(LogLevel.ThreadManager, $"Thread Stopped: {e.Thread.Name}");
        }

        private static void ThreadManager_ThreadForcedToClose(object sender, Shared.ThreadManagerEventArgs e)
        {
            _logger.AddToLog(LogLevel.ThreadManager, $"Thread Forced To Stop: {e.Thread.Name}");
        }

        private static void ThreadManager_ThreadCancellAll(object sender, EventArgs e)
        {
            _logger.AddToLog(LogLevel.ThreadManager, "Thread Cancell All");
        }

        private static void ThreadManager_ThreadExceptionRaised(object sender, Shared.ThreadManagerExceptionEventArgs e)
        {
            _logger.AddToLog(LogLevel.ThreadManager, $"Thread Exception: {e.Thread.Name} - {e.Error.Message}");
        }

        private static bool CheckForUpdate()
        {
            string installFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "gsend.pro.update.exe");
            string deletePreviousInstallFile = installFile.Replace(".exe", ".old.exe");

            if (File.Exists(deletePreviousInstallFile))
            {
                File.Delete(deletePreviousInstallFile);
            }

            if (File.Exists(installFile) && MessageBox.Show(null, 
                GSend.Language.Resources.UpdateAvailable, 
                GSend.Language.Resources.NewUpdate,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Move(installFile, deletePreviousInstallFile);

                Process.Start(deletePreviousInstallFile);
                return true;
            }

            return false;
        }
    }
}