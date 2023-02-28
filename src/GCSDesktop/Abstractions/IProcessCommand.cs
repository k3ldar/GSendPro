using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSendDesktop.Abstractions
{
    public interface ICommandProcessor
    {
        void ProcessCommand(Action command);
    }
}
