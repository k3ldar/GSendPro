﻿using GSendShared.Interfaces;
using GSendAnalyser.Analysers;

namespace GSendAnalyser.Internal
{
    internal class GCodeAnalyzerFactory : IGCodeAnalyzerFactory
    {
        public IReadOnlyList<IGCodeAnalyzer> Create()
        {
            List<IGCodeAnalyzer> result = new List<IGCodeAnalyzer>()
            {
                new AnalyzeHomeZ(),
                new AnalyzeSafeZ(),
                new AnalyzeDuplicates(),
                new AnalyzeDistance(),
                new AnalyzeUnitOfMeasure(),
                new AnalyzeTime(),
                new AnalyzeEndProgram(),
                new AnalyzeFileDetails(),
                new AnalyzeCoolantUsage(),
                new AnalyzeToolChange(),

            }
            .OrderBy(o => o.Order)
            .ToList();

            return result;
        }
    }
}
