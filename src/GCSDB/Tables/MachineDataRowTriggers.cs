using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimpleDB;

namespace GSendDB.Tables
{
    internal class MachineDataRowTriggers : ITableTriggers<MachineDataRow>
    {
        public int Position => 0;

        public TriggerType TriggerTypes => TriggerType.BeforeInsert | 
            TriggerType.BeforeUpdate;

        public void AfterDelete(List<MachineDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void AfterInsert(List<MachineDataRow> records)
        {
            records.ForEach(r =>
            {
                r.ServiceWeeks = 20;
                r.ServiceSpindleHours = 50;
                r.SoftStartSeconds = 15;
            });
        }

        public void AfterUpdate(List<MachineDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void BeforeDelete(List<MachineDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void BeforeInsert(List<MachineDataRow> records)
        {
            Parallel.ForEach(records, r =>
            {
                ValidateRecord(r);
            });
        }

        public void BeforeUpdate(List<MachineDataRow> records)
        {
            Parallel.ForEach(records, r =>
            {
                ValidateRecord(r);
            });
        }

        public void BeforeUpdate(MachineDataRow newRecord, MachineDataRow oldRecord)
        {
            // not used but declared as part of interface
        }

        private void ValidateRecord(MachineDataRow newRecord)
        {
            if (String.IsNullOrEmpty(newRecord.Name))
                throw new InvalidDataRowException(nameof(MachineDataRow), nameof(MachineDataRow.Name), "Name can not be null or empty");

            if (newRecord.MachineType == GSendShared.MachineType.Unspecified)
                throw new InvalidDataRowException(nameof(MachineDataRow), nameof(MachineDataRow.MachineType), "Unspecified machine type");
        }
    }
}
