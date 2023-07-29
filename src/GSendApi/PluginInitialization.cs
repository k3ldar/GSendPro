using System.Text.Json;

using GSendShared;

using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;

namespace GSendApi
{
    public sealed class PluginInitialization : IPlugin
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string json = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder, Constants.AppSettings));
            Dictionary<string, object> jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(json, Constants.DefaultJsonSerializerOptions);
            dynamic apiSettings = jsonData["ApiSettings"];
            ApiSettings settings = JsonSerializer.Deserialize<ApiSettings>(apiSettings.ToString(), Constants.DefaultJsonSerializerOptions);

            services.AddSingleton(settings);
            services.AddSingleton<IGSendApiWrapper, GSendApiWrapper>();
        }

        public void Finalise()
        {
            // required by interface, not used in this context
        }

        public ushort GetVersion()
        {
            return 1;
        }

        public void Initialise(ILogger logger)
        {
            // required by interface, not used in this context
        }
    }
}
