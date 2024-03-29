﻿using SimpleDB;

namespace GSendDB.Tables
{
    internal sealed class MachineDataRowDefaults : ITableDefaults<MachineDataRow>
    {
        public long PrimarySequence => 0;

        public long SecondarySequence => 0;

        public ushort Version => 1;

        public List<MachineDataRow> InitialData(ushort version)
        {
            return [];
        }
    }
}
