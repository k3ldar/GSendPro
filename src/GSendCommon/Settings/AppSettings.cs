using System.Text.Json;

using GSendApi;

using GSendShared;

namespace GSendCommon.Settings
{
    /// <summary>
    /// Configuration class for appsettings.json
    /// </summary>
    public sealed class AppSettings
    {
        public Logging Logging { get; set; }

        public Kestrel Kestrel { get; set; }

        public string AllowedHosts { get; set; }

        public GSendSettings GSend { get; set; }

        public ApiSettings ApiSettings { get; set; }

        public static AppSettings Load()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder, Constants.AppSettings);

            return JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(path), Constants.DefaultJsonSerializerOptions);
        }

        public void Save()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder, Constants.AppSettings);
            File.WriteAllText(path, JsonSerializer.Serialize(this, Constants.DefaultJsonSerializerOptions));
        }
    }
}
