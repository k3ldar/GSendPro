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
            List<T> Result = new();

            if (typeof(T) == typeof(IGCodeOverride))
            {
                GetCommonOfType(Result, typeof(IGCodeOverride));
            }
            else if (typeof(T) == typeof(IMCodeOverride))
            {
                GetCommonOfType(Result, typeof(IMCodeOverride));
            }
            else if (typeof(T) == typeof(IGCodeAnalyzer))
            {
                GetAnalysersOfType(Result, typeof(IGCodeAnalyzer));
            }

            return Result;
        }

        private static void GetAnalysersOfType<T>(List<T> analyzerList, Type typeRequired)
        {
            foreach (Type type in typeof(GSendAnalyser.GCodeAnalyses).Assembly.GetTypes())
            {
                if (type.IsClass && type.GetInterface(typeRequired.Name) != null)
                {
                    analyzerList.Add((T)Activator.CreateInstance(type));
                }
            }
        }

        private static void GetCommonOfType<T>(List<T> analyzerList, Type typeRequired)
        {
            foreach (Type type in typeof(GSendCommon.PluginInitialisation).Assembly.GetTypes())
            {
                if (type.IsClass && type.GetInterface(typeRequired.Name) != null)
                {
                    try
                    {
                        analyzerList.Add((T)Activator.CreateInstance(type));
                    }
                    catch
                    {
                        //ignore
                    }
                }
            }
        }

        public List<Type> GetPluginClassTypes<T>()
        {
            throw new NotImplementedException();
        }


    }
}
