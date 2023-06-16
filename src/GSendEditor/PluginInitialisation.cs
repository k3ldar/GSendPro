using System.Text.Json;

using GSendApi;
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
            dynamic apiSettings = jsonData["ApiSettings"];
            ApiSettings settings = JsonSerializer.Deserialize<ApiSettings>(apiSettings.ToString(), Constants.DefaultJsonSerializerOptions);
            GSendSettings gSendSettings = JsonSerializer.Deserialize<GSendSettings>(jsonData["GSend"].ToString(), Constants.DefaultJsonSerializerOptions);

            services.AddSingleton<IGSendContext, GSendContext>();
            services.AddSingleton(new GSendSettings());
            services.AddSingleton(settings);
            services.AddSingleton<IGsendSettings>(gSendSettings);
            services.AddSingleton<GSendApiWrapper>();
            services.AddTransient<IComPortProvider, ComPortProvider>();
            services.AddTransient<FrmMain>();
            services.AddTransient<SubProgramForm>();
            services.AddTransient<Bookmarks>();
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
