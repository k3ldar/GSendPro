using System.Reflection;

using SharedPluginFeatures;

namespace gsend.pro.Internal
{
    public class GSendResources : ILanguageFile
    {
        public string Name => "GSend.Language.Resources";

        public Assembly Assembly => typeof(GSend.Language.Resources).Assembly;
    }
}
