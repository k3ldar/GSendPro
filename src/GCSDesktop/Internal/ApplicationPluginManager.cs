using System;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using PluginManager;
using PluginManager.Abstractions;

namespace GSendDesktop.Internal
{
    internal sealed class ApplicationPluginManager : BasePluginManager
    {
        public ApplicationPluginManager(
            PluginManagerConfiguration configuration, 
            PluginSettings pluginSettings)
            : base(configuration, pluginSettings)
        {
            
        }

        public IServiceProvider GetServiceProvider => base.ServiceProvider;

        public void RegisterPlugin(string pluginName)
        {
            PluginLoad(pluginName, false);
        }

        protected override bool CanExtractResource(in string resourceName)
        {
            return false;
        }

        protected override void ModifyPluginResourceName(ref string resourceName)
        {

        }

        protected override void PluginConfigured(in IPluginModule pluginModule)
        {

        }

        protected override void PluginInitialised(in IPluginModule pluginModule)
        {

        }

        protected override void PluginLoaded(in Assembly pluginFile)
        {

        }

        protected override void PluginLoading(in Assembly pluginFile)
        {

        }

        protected override void PostConfigurePluginServices(in IServiceCollection serviceProvider)
        {

        }

        protected override void PreConfigurePluginServices(in IServiceCollection serviceProvider)
        {

        }

        protected override void ServiceConfigurationComplete(in IServiceCollection serviceCollection)
        {

        }
    }
}
