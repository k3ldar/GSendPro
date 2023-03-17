﻿using System.ComponentModel;

using GSendDB.Providers;
using GSendDB.Tables;

using GSendShared;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;

using SharedPluginFeatures;

using SimpleDB;

#pragma warning disable IDE0060

namespace GSendDB
{
    public class PluginInitialization : IPlugin, IInitialiseEvents
    {
        #region IPlugin Methods

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMachineProvider, MachineProvider>();
        }

        public void Finalise()
        {
            // from interface but unused in this context
        }

        public void Initialise(ILogger logger)
        {
            // from interface but unused in this context
        }

        public ushort GetVersion()
        {
            return 1;
        }

        #endregion IPlugin Methods

        #region IInitialiseEvents Methods

        public void BeforeConfigure(in IApplicationBuilder app)
        {
            // from interface but unused in this context
        }

        public void AfterConfigure(in IApplicationBuilder app)
        {
            // initialize all tables
            _ = app.ApplicationServices.GetService<ISimpleDBOperations<MachineDataRow>>();
        }

        public void Configure(in IApplicationBuilder app)
        {
            // from interface but unused in this context
        }

        public void BeforeConfigureServices(in IServiceCollection services)
        {
            // register tables
            services.AddSingleton(typeof(TableRowDefinition), typeof(MachineDataRow));
        }

        public void AfterConfigureServices(in IServiceCollection services)
        {
            // from interface but unused in this context
        }

        #endregion IInitialiseEvents Methods
    }
}

#pragma warning restore IDE0060