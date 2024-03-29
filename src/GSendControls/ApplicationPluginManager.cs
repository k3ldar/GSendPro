﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GSendControls.Abstractions;
using GSendControls.Plugins;

using GSendShared;
using GSendShared.Plugins;

using Microsoft.Extensions.DependencyInjection;

using PluginManager;
using PluginManager.Abstractions;

namespace GSendControls
{
    public sealed class ApplicationPluginManager : BasePluginManager
    {
        public ApplicationPluginManager(
            PluginManagerConfiguration configuration,
            PluginSettings pluginSettings)
            : base(configuration, pluginSettings)
        {

        }

        new public IServiceProvider ServiceProvider
        {
            get => base.ServiceProvider;
            set => base.ServiceProvider = value;
        }

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
            serviceCollection.AddSingleton<IServiceProvider>(sp => { return ServiceProvider; });
            serviceCollection.AddTransient<IServiceProvider>(sp => { return ServiceProvider; });
            serviceCollection.AddScoped<IServiceProvider>(sp => { return ServiceProvider; });
        }

        public void LoadAllPlugins(PluginHosts pluginHosts, string pluginConfig)
        {
            if (pluginHosts == PluginHosts.None)
                throw new InvalidOperationException("Invalid plugin usage.");

            ILogger logger = ServiceProvider.GetRequiredService<ILogger>();

            if (!File.Exists(pluginConfig))
            {
                logger.AddToLog(LogLevel.PluginConfigureError, $"Plugin configuration file not found: {pluginConfig}");
                return;
            }

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;

            List<GSendPluginSettings> pluginSettings = HelperMethods.LoadPluginSettings(pluginConfig);

            foreach (GSendPluginSettings pluginSetting in pluginSettings)
            {
                try
                {
                    if (!pluginSetting.Enabled)
                    {
                        logger.AddToLog(LogLevel.PluginLoadFailed, $"{pluginSetting.Name} is not enabled.");
                        continue;
                    }

                    if (pluginSetting.Usage != pluginHosts)
                    {
                        logger.AddToLog(LogLevel.PluginLoadFailed, $"{pluginSetting.Name} is not configured for {pluginHosts}");
                        continue;
                    }

                    if (!IsAssemblyLoaded(pluginSetting.AssemblyName, out Assembly pluginAssembly))
                    {
                        pluginAssembly = Assembly.Load(pluginSetting.AssemblyName);
                    }

                    if (FindPluginModuleClassByInterface(pluginAssembly, nameof(IGSendPluginModule)) == null)
                    {
                        logger.AddToLog(LogLevel.PluginLoadError, $"{pluginSetting.Name} does not contain an {nameof(IGSendPluginModule)} implementation.");
                        continue;
                    }

                    Type pluginInitializationType = FindPluginModuleClassByInterface(pluginAssembly, nameof(IPlugin));

                    if (pluginInitializationType == null)
                    {
                        logger.AddToLog(LogLevel.PluginLoadError, $"{pluginSetting.Name} does not contain an {nameof(IPlugin)} implementation.");
                        continue;
                    }

                    PluginLoad(pluginAssembly, pluginAssembly.Location, true);
                    logger.AddToLog(LogLevel.PluginLoadSuccess, pluginSetting.AssemblyName);
                }
                catch (Exception ex)
                {
                    logger.AddToLog(LogLevel.PluginLoadError, ex);
                }
            }

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= CurrentDomain_ReflectionOnlyAssemblyResolve;
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_ReflectionOnlyAssemblyResolve;
        }

        private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            string name = new AssemblyName(args.Name).Name;
            string pluginPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Plugins", $"{name}.dll");

            if (File.Exists(pluginPath))
            {
                Logger.AddToLog(LogLevel.Information, $"Plugin found: {pluginPath}");
                return Assembly.Load(File.ReadAllBytes(pluginPath));
            }
            else
            {
                Logger.AddToLog(LogLevel.Warning, $"Plugin not found: {pluginPath}");
            }

            return null;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S6605:Collection-specific \"Exists\" method should be used instead of the \"Any\" extension", Justification = "Not relevant for array")]
        private static Type FindPluginModuleClassByInterface(Assembly assembly, string interfaceName)
        {
            foreach (var classType in assembly.ExportedTypes)
            {
                if (classType.GetInterfaces().Any(i => i.UnderlyingSystemType.Name == interfaceName))
                    return classType;
            }

            return null;
        }

        private static bool IsAssemblyLoaded(string assemblyName, out Assembly pluginAssembly)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.Location.EndsWith(assemblyName))
                {
                    pluginAssembly = assembly;
                    return true;
                }
            }

            pluginAssembly = null;
            return false;
        }
    }
}
