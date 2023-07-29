using AspNetCore.PluginManager;

using gsend.pro.Internal;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using PluginManager;

using Shared.Classes;

namespace gsend.pro
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

            System.Net.ServicePointManager.DefaultConnectionLimit = 100;
            System.Net.ServicePointManager.ReusePort = true;
            System.Net.ServicePointManager.MaxServicePoints = 5;
#pragma warning disable S4830 // Server certificates should be verified during SSL/TLS connections
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
#pragma warning restore S4830 // Server certificates should be verified during SSL/TLS connections

            PluginManagerService.UsePlugin(typeof(PluginManager.DAL.TextFiles.PluginInitialisation));

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
            PluginManagerService.UsePlugin(typeof(SearchPlugin.PluginInitialisation));

            PluginManagerConfiguration configuration = new()
            {
                ServiceConfigurator = new ServiceConfigurator(),
            };

            PluginManagerService.Initialise(configuration);

            PluginManagerService.UsePlugin(typeof(UserAccount.Plugin.PluginInitialisation));
            PluginManagerService.UsePlugin(typeof(LoginPlugin.PluginInitialisation));
            PluginManagerService.UsePlugin(typeof(SystemAdmin.Plugin.PluginInitialisation));


            PluginManagerService.UsePlugin(typeof(SimpleDB.PluginInitialisation));

            try
            {
                CreateWebHostBuilder(args).Build().Run();
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

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseContentRoot(System.IO.Directory.GetCurrentDirectory())
                .UseDefaultServiceProvider(options =>
                    options.ValidateScopes = false);
    }
}