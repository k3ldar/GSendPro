using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

using GSendCommon.Settings;

using GSendControls;

using GSendDesktop.Abstractions;
using GSendDesktop.Forms;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Providers;

using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;

using Shared.Classes;

using SharedLib.Win.Classes;

namespace GSendDesktop.Internal
{
    public sealed class PluginInitialisation : IPlugin
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string json = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder, Constants.AppSettings));
            Dictionary<string, object> jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(json, Constants.DefaultJsonSerializerOptions);
            GSendSettings gSendSettings = JsonSerializer.Deserialize<GSendSettings>(jsonData["GSend"].ToString(), Constants.DefaultJsonSerializerOptions);

            services.AddSingleton<IGSendContext, GSendContext>();
            services.AddSingleton(gSendSettings);
            services.AddSingleton<IGSendSettings>(gSendSettings);
            services.AddTransient<IMessageNotifier, MessageNotifier>();
            services.AddTransient<IComPortProvider, ComPortProvider>();
            services.AddTransient<ICommandProcessor, CommandProcessor>();
            services.AddTransient<FormMain>();
            services.AddSingleton<ISubprograms, SubProgramsApi>();
            services.AddSingleton<IRunProgram, WindowsRunProgram>();
        }

        public void Finalise()
        {

        }

        public ushort GetVersion()
        {
            return 1;
        }

        public void Initialise(ILogger logger)
        {

        }
    }
}
