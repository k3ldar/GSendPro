using System;

namespace GSendDesktop.Abstractions
{
    public interface ICommandProcessor
    {
        void ProcessCommand(Action command);
    }
}
