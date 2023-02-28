using GSendService.Internal;

using GSendShared;
using GSendShared.Interfaces;
using GSendShared.Providers;

using PluginManager.Abstractions;

using SharedPluginFeatures;

namespace GSendService
{
    public class PluginInitialization : IPlugin, IInitialiseEvents
    {
        #region IInitialiseEvents Methods

        public void AfterConfigure(in IApplicationBuilder app)
        {

        }

        public void AfterConfigureServices(in IServiceCollection services)
        {
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(
            //        STConsts.PolicyManageLicense,
            //        policyBuilder => policyBuilder.RequireClaim(STConsts.ClaimManageLicense));
            //});
        }

        public void BeforeConfigure(in IApplicationBuilder app)
        {

        }

        public void BeforeConfigureServices(in IServiceCollection services)
        {
            services.AddSingleton<IErrorManager, ErrorManagerProvider>();
            services.AddSingleton<IComPortProvider, ComPortProvider>();
            services.AddSingleton<IComPortFactory, ComPortFactory>();
        }

        public void Configure(in IApplicationBuilder app)
        {

        }

        #endregion IInitialiseEvents Methods

        #region IPlugin Methods

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IComPortProvider, ComPortProvider>();
        }

        public void Finalise()
        {

        }

        public ushort GetVersion()
        {
            return 1;
        }

        public void Initialise(PluginManager.Abstractions.ILogger logger)
        {

        }

        #endregion IPlugin Methods
    }
}
