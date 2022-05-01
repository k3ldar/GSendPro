using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCAAnalyser.Abstractions
{
    public interface IGCodeAnalyzerFactory
    {
        IReadOnlyList<IGCodeAnalyzer> Create();
    }
}
