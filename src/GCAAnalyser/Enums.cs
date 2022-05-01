using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCAAnalyser
{
    [Flags]
    public enum CommandAttributes
    {
        /// <summary>
        /// Duplicate of previous command
        /// </summary>
        Duplicate,

        /// <summary>
        /// Command contains a value for setting spindle speed
        /// </summary>
        SpindleSpeed,

        /// <summary>
        /// Moves to safe Z
        /// </summary>
        SafeZ,

        /// <summary>
        /// Moves to home
        /// </summary>
        Home
    }
}
