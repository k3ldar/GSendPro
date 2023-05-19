using System;
using System.Collections.Generic;

using GSendAnalyser;
using GSendAnalyser.Analysers;

using GSendShared.Abstractions;

using PluginManager.Abstractions;

namespace GSendTests.Mocks
{
    internal class MockPluginClassesService : IPluginClassesService
    {
        public object[] GetParameterInstances(Type type)
        {
            throw new NotImplementedException();
        }

        public List<T> GetPluginClasses<T>()
        {
            if (typeof(T) == typeof(IGCodeAnalyzerFactory))
            {
            }

            if (typeof(T) == typeof(IGCodeAnalyzer))
            {
                List<T> analyzerList = new List<T>();

                foreach (Type type in typeof(GSendAnalyser.GCodeAnalyses).Assembly.GetTypes())
                {
                    if (type.IsClass && type.GetInterface(nameof(IGCodeAnalyzer)) != null)
                    {
                        analyzerList.Add((T)Activator.CreateInstance(type));
                    }
                }

                return analyzerList;
            }

            return new List<T>();
        }

        public List<Type> GetPluginClassTypes<T>()
        {
            throw new NotImplementedException();
        }
    }
}
