using System;
using System.IO;
using System.Linq;
using System.Text;

using AspNetCore.PluginManager;

using GSendService.Internal;

using GSendShared;
using GSendShared.Providers.Internal.Enc;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using PluginManager;

using Shared.Classes;

using LogLevel = PluginManager.LogLevel;

namespace GSendService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ThreadManager.Initialise(new SharedLib.Win.WindowsCpuUsage());
            ThreadManager.AllowThreadPool = true;
            ThreadManager.MaximumPoolSize = 5000;
            ThreadManager.ThreadExceptionRaised += ThreadManager_ThreadExceptionRaised;
            ThreadManager.ThreadStopped += ThreadManager_ThreadStopped;

            Environment.SetEnvironmentVariable(Constants.GSendPathEnvVar,
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder));

            Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable(Constants.GSendPathEnvVar), "db"));

            System.Net.ServicePointManager.DefaultConnectionLimit = 100;
            System.Net.ServicePointManager.ReusePort = true;
            System.Net.ServicePointManager.MaxServicePoints = 0;
#pragma warning disable S4830 // Server certificates should be verified during SSL/TLS connections
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
#pragma warning restore S4830 // Server certificates should be verified during SSL/TLS connections

            Logger logger = new();

            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                if (eventArgs.Exception.Message.Equals("Unable to read data from the transport connection: The I/O operation has been aborted because of either a thread exit or an application request"))
                {
                    //ignore
                }
                else if (eventArgs.Exception.Message.StartsWith("The response ended prematurely."))
                {
                    //ignore
                }
                else
                {
                    logger.AddToLog(LogLevel.Critical, eventArgs.Exception);
                }
            };

            PluginManagerService.UsePlugin(typeof(PluginManager.DAL.TextFiles.PluginInitialisation));
            PluginManagerService.UsePlugin(typeof(GSendShared.PluginInitialisation));
            PluginManagerService.UsePlugin(typeof(GSendDB.PluginInitialization));
            PluginManagerService.UsePlugin(typeof(GSendApi.PluginInitialization));

#if !DEBUG
            PluginManagerService.UsePlugin(typeof(ErrorManager.Plugin.PluginInitialisation));
#endif

            PluginManagerService.UsePlugin(typeof(RestrictIp.Plugin.PluginInitialisation));
            PluginManagerService.UsePlugin(typeof(UserSessionMiddleware.Plugin.PluginInitialisation));
            PluginManagerService.UsePlugin(typeof(CacheControl.Plugin.PluginInitialisation));
            PluginManagerService.UsePlugin(typeof(MemoryCache.Plugin.PluginInitialisation));
            PluginManagerService.UsePlugin(typeof(Localization.Plugin.PluginInitialisation));
            PluginManagerService.UsePlugin(typeof(Breadcrumb.Plugin.PluginInitialisation));
            PluginManagerService.UsePlugin(typeof(ApiAuthorization.Plugin.PluginInitialisation));

            PluginManagerConfiguration configuration = new(logger)
            {
                ServiceConfigurator = new ServiceConfigurator(),
                ConfigFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder, Constants.AppSettings)
            };

            PluginManagerService.Initialise(configuration);

            PluginManagerService.UsePlugin(typeof(UserAccount.Plugin.PluginInitialisation));
            PluginManagerService.UsePlugin(typeof(LoginPlugin.PluginInitialisation));
            PluginManagerService.UsePlugin(typeof(SystemAdmin.Plugin.PluginInitialisation));
            PluginManagerService.UsePlugin(typeof(GSendAnalyzer.PluginInitialisation));
            PluginManagerService.UsePlugin(typeof(GSendCommon.PluginInitialisation));


            PluginManagerService.UsePlugin(typeof(SimpleDB.PluginInitialisation));

            CheckForUpdatesThread checkForUpdatesThread = new();
            ThreadManager.ThreadStart(checkForUpdatesThread, nameof(checkForUpdatesThread), System.Threading.ThreadPriority.BelowNormal, true);

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                ThreadManager.CancelAll();
                PluginManagerService.Finalise();
            }
        }

        private static void ThreadManager_ThreadStopped(object sender, Shared.ThreadManagerEventArgs e)
        {
            // not used in this context
        }

        private static void ThreadManager_ThreadExceptionRaised(object sender, Shared.ThreadManagerExceptionEventArgs e)
        {
            // not used in this context
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureAppConfiguration(configureDelegate =>
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder, Constants.AppSettings);
                    configureDelegate.AddJsonFile(path, false, true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<GcsWindowsService>();
                    services.Configure<KestrelServerOptions>(
                        hostContext.Configuration.GetSection("Kestrel"));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}