using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GCAAnalyser.Abstractions;
using GCAAnalyser.Internal;

namespace GCAAnalyser
{
    internal class GCodeAnalyses : IGCodeAnalyses
    {
        private readonly List<GCodeCommand> _commands = new List<GCodeCommand>();

        internal void Add(GCodeCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            _commands.Add(command);
        }

        public void Analyse()
        {
            IGCodeAnalyzerFactory gCodeAnalyzerFactory = new GCodeAnalyzerFactory();

            IReadOnlyList<IGCodeAnalyzer> analyzers = gCodeAnalyzerFactory.Create();

            Parallel.ForEach(analyzers, gCodeAnalyzer =>
            {
                gCodeAnalyzer.Analyze(this);
            });
        }

        public IReadOnlyList<GCodeCommand> Commands => _commands.AsReadOnly();

        public bool ContainsCarriageReturn { get; internal set; }

        public decimal SafeZ { get; set; }

        public bool ContainsDuplicates { get; set; }
    }
}
