using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Abstractions;
using GSendShared;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeVariables : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            //move error checking of variables from parser to here
        }
    }
}
