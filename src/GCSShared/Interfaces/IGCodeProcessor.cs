using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSendShared
{
    public interface IGCodeProcessor
    {

        bool Connect();

        bool Disconnect();

        long Id { get; }

        bool IsConnected { get; }
    }
}
