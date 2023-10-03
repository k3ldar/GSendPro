using System;
using System.IO;

using AppSettings;

using GSendCommon;

using GSendService.Internal;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Providers;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;

using SharedPluginFeatures;

namespace GSendService
{
    public class PluginInitialization : IPlugin, IInitialiseEvents
    {
        #region IInitialiseEvents Methods

        public void AfterConfigure(in IApplicationBuilder app)
        {
            // not used in this context
        }

        public void AfterConfigureServices(in IServiceCollection services)
        {
            // not implemented at this time
        }

        public void BeforeConfigure(in IApplicationBuilder app)
        {
            // not implemented at this time
        }

        public void BeforeConfigureServices(in IServiceCollection services)
        {
            services.AddTransient<ISettingOverride, SettingOverride>();
            services.AddTransient<IApplicationOverride, ApplicationOverride>();
            services.AddTransient<ISettingError, SettingsError>();
            services.AddSingleton<ISettingsProvider, DefaultSettingProvider>();
            services.AddSingleton<IErrorManager, ErrorManagerProvider>();
            services.AddSingleton<IComPortProvider, ComPortProvider>();
            services.AddSingleton<IComPortFactory, ComPortFactory>();
            services.AddTransient<IProcessorMediator, ProcessorMediator>();
            services.AddSingleton<IStaticMethods, StaticMethods>();
            services.AddSingleton<ISubprograms, Subprograms>();
            services.AddSingleton<ISharedPluginHelper, SharedPluginHelper>();
        }

        public void Configure(in IApplicationBuilder app)
        {
            // not implemented at this time
        }

        #endregion IInitialiseEvents Methods

        #region IPlugin Methods

        public void ConfigureServices(IServiceCollection services)
        {
            // not implemented at this time
        }

        public void Finalise()
        {
            // not implemented at this time
        }

        public ushort GetVersion()
        {
            return 1;
        }

        public void Initialise(PluginManager.Abstractions.ILogger logger)
        {
            string rootPath = Environment.GetEnvironmentVariable(GSendShared.Constants.GSendPathEnvVar);
            Shared.EventLog.Path = Path.Combine(rootPath, nameof(GSendService));
        }

        #endregion IPlugin Methods
    }
}
