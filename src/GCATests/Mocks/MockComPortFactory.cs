using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared;

namespace GSendTests.Mocks
{
    [ExcludeFromCodeCoverage]
    internal sealed class MockComPortFactory : IComPortFactory
    {
        public IComPort CreateComPort(IMachine machine)
        {
            return new MockComPort(machine);
        }
    }
}
