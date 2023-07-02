using AppSettings;

using gsend.pro.Internal;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;

using SharedPluginFeatures;

namespace gsend.pro
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
            services.AddSingleton<IErrorManager, ErrorManagerProvider>();
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
            // not implemented at this time
        }

        #endregion IPlugin Methods
    }
}
