using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimpleDB;

namespace GSendDB.Tables
{
    internal class ServiceItemsDefaults : ITableDefaults<ServiceItemsDataRow>
    {
        public long PrimarySequence => 0;

        public long SecondarySequence => 0;

        public ushort Version => 1;

        public List<ServiceItemsDataRow> InitialData(ushort version)
        {
            if (version == 1)
            {
                return new List<ServiceItemsDataRow>()
                {
                    new ServiceItemsDataRow() { Name = "Clean Machine/Enclosure", IsDaily = true, IsMinor = true, IsMajor = true },
                    new ServiceItemsDataRow() { Name = "Inspect Bits for Damage", IsDaily = true, IsMinor = true, IsMajor = true },
                    new ServiceItemsDataRow() { Name = "Inspect Wires and Connections", IsDaily = true, IsMinor = true, IsMajor = true },
                    new ServiceItemsDataRow() { Name = "Inspect Surfacing Board", IsDaily = true, IsMinor = true, IsMajor = true },
                    new ServiceItemsDataRow() { Name = "Clean X Axis Moving Parts", IsDaily = false, IsMinor = true, IsMajor = true },
                    new ServiceItemsDataRow() { Name = "Clean Y Axis Moving Parts", IsDaily = false, IsMinor = true, IsMajor = true },
                    new ServiceItemsDataRow() { Name = "Clean Z Axis Moving Parts", IsDaily = false, IsMinor = true, IsMajor = true },
                    new ServiceItemsDataRow() { Name = "Lubricate moving parts (dry lube)", IsDaily = false, IsMinor = true, IsMajor = true },
                    new ServiceItemsDataRow() { Name = "Inspect Collets", IsDaily = false, IsMinor = true, IsMajor = true },
                    new ServiceItemsDataRow() { Name = "Tram Router", IsDaily = false, IsMinor = false, IsMajor = true },
                    new ServiceItemsDataRow() { Name = "Inspect Lead/Ball Screws", IsDaily = false, IsMinor = false, IsMajor = true },
                    new ServiceItemsDataRow() { Name = "Test individual Limit Switches", IsDaily = false, IsMinor = false, IsMajor = true },
                    new ServiceItemsDataRow() { Name = "Validate Gantry is Square", IsDaily = false, IsMinor = false, IsMajor = true },
                    new ServiceItemsDataRow() { Name = "Confirm all screws/bolts are correctly tightened", IsDaily = false, IsMinor = false, IsMajor = true },
                };
            }

            return new List<ServiceItemsDataRow>();
        }
    }
}
