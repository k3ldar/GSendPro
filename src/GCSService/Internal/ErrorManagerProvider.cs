using SharedPluginFeatures;
using System.Text.Json;

namespace GSendService.Internal
{
    public class ErrorManagerProvider : IErrorManager
    {
        public void ErrorRaised(in ErrorInformation errorInformation)
        {
            string path = "C:\\Tools\\gcs\\error";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string errText = JsonSerializer.Serialize<ErrorInformation>(errorInformation, new JsonSerializerOptions());

            string file = Path.Combine(path, $"{DateTime.UtcNow.Ticks}.error.txt");
            File.WriteAllText(file, errText);
        }

        public bool MissingPage(in string path, ref string replacePath)
        {
            if (path.Equals("/home", StringComparison.InvariantCultureIgnoreCase))
            {
                replacePath = "/";
                return true;
            }
            if (path.Equals("/home/index", StringComparison.InvariantCultureIgnoreCase))
            {
                replacePath = "/";
                return true;
            }

            return false;
        }
    }
}
