using System.Reflection;

using Languages;

using SharedPluginFeatures;

namespace GSendService.Internal
{
    public class GSendResources : ILanguageFile
    {
        public string Name => "GSend.Language.Resources";

        public Assembly Assembly => typeof(GSend.Language.Resources).Assembly;
    }
}
