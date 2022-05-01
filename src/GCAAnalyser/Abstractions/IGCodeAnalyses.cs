using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCAAnalyser.Abstractions
{
    public interface IGCodeAnalyses
    {
        IReadOnlyList<GCodeCommand> Commands { get; }

        bool ContainsCarriageReturn { get; }

        decimal SafeZ { get; set; }

        bool ContainsDuplicates { get; set; }

        void Analyse();
    }
}
