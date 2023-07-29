using System.Text.Json;

using GSendCommon.Settings;

using GSendControls;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Providers;

using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;

namespace GSendEditor.Internal
{
    public sealed class PluginInitialisation : IPlugin
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string json = System.IO.File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder, Constants.AppSettings));
            Dictionary<string, object> jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(json, Constants.DefaultJsonSerializerOptions);
            GSendSettings gSendSettings = JsonSerializer.Deserialize<GSendSettings>(jsonData["GSend"].ToString(), Constants.DefaultJsonSerializerOptions);

            services.AddSingleton<IGSendContext, GSendContext>();
            services.AddSingleton(new GSendSettings());
            services.AddSingleton<IGSendSettings>(gSendSettings);
            services.AddTransient<IComPortProvider, ComPortProvider>();
            services.AddTransient<FrmMain>();
            services.AddTransient<SubProgramForm>();
            services.AddTransient<Bookmarks>();
            services.AddSingleton<ISubprograms, SubProgramsApi>();
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
