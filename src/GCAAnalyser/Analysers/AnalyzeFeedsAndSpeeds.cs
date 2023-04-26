using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Interfaces;
using GSendShared;

namespace GSendAnalyser.Analysers
{
    public sealed class AnalyzeFeedsAndSpeeds : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            gCodeAnalyses.FeedX = gCodeAnalyses.Commands.Where(c => c.Command.Equals('X')).Max(c => c.FeedRate);
            gCodeAnalyses.FeedZ = gCodeAnalyses.Commands.Where(c => c.Command.Equals('Z')).Max(c => c.FeedRate);
        }
    }
}
