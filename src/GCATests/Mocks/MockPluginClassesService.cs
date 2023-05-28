﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GSendShared.Abstractions;

using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;

namespace GSendTests.Mocks
{
    internal class MockPluginClassesService : IPluginClassesService
    {
        public object[] GetParameterInstances(Type type)
        {
            List<object> Result = new List<object>();

            List<ConstructorInfo> constructors = type.GetConstructors()
                .Where(c => c.IsPublic && !c.IsStatic && c.GetParameters().Length > 0)
                .OrderByDescending(c => c.GetParameters().Length)
                .ToList();

            foreach (ConstructorInfo constructor in constructors)
            {
                foreach (ParameterInfo param in constructor.GetParameters())
                {
                    List<object> list = new List<object>();
                    GetCommonOfType<object>(list, param.ParameterType);
                    object paramClass = list.First();

                    if (paramClass == null)
                    {
                        Result.Clear();
                        break;
                    }

                    Result.Add(paramClass);
                }

                if (Result.Count > 0)
                    return Result.ToArray();
            }

            return Result.ToArray();
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

        private void GetAnalysersOfType<T>(List<T> analyzerList, Type typeRequired)
        {
            foreach (Type type in typeof(GSendAnalyser.GCodeAnalyses).Assembly.GetTypes())
            {
                if (type.IsClass && type.GetInterface(typeRequired.Name) != null)
                {
                    analyzerList.Add((T)Activator.CreateInstance(type, GetParameterInstances(typeRequired)));
                }
            }
        }

        private void GetCommonOfType<T>(List<T> analyzerList, Type typeRequired)
        {
            foreach (Type type in typeof(GSendCommon.PluginInitialisation).Assembly.GetTypes())
            {
                if (type.IsClass && type.GetInterface(typeRequired.Name) != null)
                {
                    try
                    {
                        analyzerList.Add((T)Activator.CreateInstance(type, GetParameterInstances(typeRequired)));
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