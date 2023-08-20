using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSendAnalyzer
{
    public sealed class InvalidGCodeException : Exception
    {
        public InvalidGCodeException()
        {
        }

        public InvalidGCodeException(string message)
            : base(message)
        {
        }

        public InvalidGCodeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
