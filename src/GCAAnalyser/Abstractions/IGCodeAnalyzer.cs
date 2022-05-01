﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCAAnalyser.Abstractions
{
    public interface IGCodeAnalyzer
    {
        void Analyze(IGCodeAnalyses gCodeAnalyses);

        int Order { get; }
    }
}
